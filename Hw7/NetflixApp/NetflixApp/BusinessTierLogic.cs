//
// BusinessTier:  business logic, acting as interface between UI and data store.
//

using System;
using System.Collections.Generic;
using System.Data;


namespace BusinessTier
{

  //
  // Business:
  //
  public class Business
  {
    //
    // Fields:
    //
    private string _DBFile;
    private DataAccessTier.Data datatier;


    //
    // Constructor:
    //
    public Business(string DatabaseFilename)
    {
      _DBFile = DatabaseFilename;

      datatier = new DataAccessTier.Data(DatabaseFilename);
    }


    //
    // TestConnection:
    //
    // Returns true if we can establish a connection to the database, false if not.
    //
    public bool TestConnection()
    {
      return datatier.TestConnection(); //Tests the connection
    }


    //
    // GetMovie:
    //
    // Retrieves Movie object based on MOVIE ID; returns null if movie is not
    // found.
    //
    public Movie GetMovie(int MovieID)
    {
        var temp = datatier.ExecuteScalarQuery(string.Format("SELECT MovieName FROM Movies WHERE MovieID={0};",MovieID.ToString()));
        if (temp != null)           //Testss to see if the movie is already in the database
        {
            BusinessTier.Movie newMovie = new BusinessTier.Movie(MovieID, temp.ToString());     //Returns new movie
            return newMovie;
        }
        return null;
    }


    //
    // GetMovie:
    //
    // Retrieves Movie object based on MOVIE NAME; returns null if movie is not
    // found.
    //
    public Movie GetMovie(string MovieName)
    {
        var quoteFix = MovieName.Replace("'", "''");
        var temp = datatier.ExecuteScalarQuery(string.Format("SELECT MovieID FROM Movies WHERE MovieName='{0}';", quoteFix));

        if (temp != null)   //Tests to see if the movie is in the database or not
        {
            BusinessTier.Movie newMovie = new BusinessTier.Movie(System.Convert.ToInt32(temp), quoteFix);
            return newMovie;
        }
        return null;
    }

    //
    // AddMovie:
    //
    // Adds the movie, returning a Movie object containing the name and the 
    // movie's id.  If the add failed, null is returned.
    //
    public Movie AddMovie(string MovieName)
    {
        var quoteFix = MovieName.Replace("'", "''");
        var temp = datatier.ExecuteScalarQuery(string.Format("SELECT MovieName FROM Movies WHERE MovieName = '{0}'", quoteFix));

        if (temp == null)//Tests to see if the movie is in the database already
        {
            var addedMovie = datatier.ExecuteActionQuery(string.Format("INSERT INTO Movies(MovieName) Values('{0}');", quoteFix));
            var movieID = datatier.ExecuteScalarQuery(string.Format("SELECT COUNT(MovieName) FROM Movies"));
            BusinessTier.Movie newMovie = new BusinessTier.Movie(System.Convert.ToInt32(movieID), MovieName);       //Returns a movie object of the new movie
            return newMovie;
        }
      
      return null;
    }


    //
    // AddReview:
    //
    // Adds review based on MOVIE ID, returning a Review object containing
    // the review, review's id, etc.  If the add failed, null is returned.
    //
    public Review AddReview(int MovieID, int UserID, int Rating)
    {
        //Adds a new review to the database
        var temp = datatier.ExecuteActionQuery(string.Format("INSERT INTO Reviews(MovieID, UserID, Rating) Values({0}, {1}, {2});", MovieID, UserID, Rating));
        var reviewID = datatier.ExecuteScalarQuery(string.Format("SELECT COUNT(Rating) FROM Reviews"));
        BusinessTier.Review newReview = new BusinessTier.Review(System.Convert.ToInt32(reviewID), MovieID, UserID, Rating);
        return newReview;           //Returns Review object
    }


    //
    // GetMovieDetail:
    //
    // Given a MOVIE ID, returns detailed information about this movie --- all
    // the reviews, the total number of reviews, average rating, etc.  If the 
    // movie cannot be found, null is returned.
    //
    public MovieDetail GetMovieDetail(int MovieID)
    {
        BusinessTier.Movie currMovie = GetMovie(MovieID);
        //Tests if the movie is in the database
        if (currMovie != null)
        {
            string quoteReplacement = currMovie.MovieName.Replace("'", "''");           //Finds the average number of ratings 

            var avgRating = datatier.ExecuteScalarQuery(string.Format(@"SELECT ROUND(AVG(CAST(Rating AS Float)), 2) AS AvgRating 
          FROM Reviews
					INNER JOIN Movies ON Reviews.MovieID = Movies.MovieID
					WHERE Movies.MovieName='{0}';", quoteReplacement));

            var numRatings = datatier.ExecuteScalarQuery(string.Format("SELECT COUNT(Rating) FROM Reviews WHERE MovieID = {0}", MovieID));

            DataSet ds = datatier.ExecuteNonScalarQuery(string.Format(@"SELECT UserID, Rating, ReviewID    
            FROM Reviews 
            WHERE MovieID={0}
            ORDER BY Rating Desc, UserID ASC;", MovieID));//Retrieves data as a data set

            DataTable dt = ds.Tables["TABLE"];              //Converts dataset into datatable
            List<BusinessTier.Review> allReviews = new List<BusinessTier.Review>();

            foreach (DataRow row in dt.Rows)            //Iterates through the table
            {
                allReviews.Add(new BusinessTier.Review(System.Convert.ToInt32(row["ReviewID"]), MovieID, System.Convert.ToInt32(row["UserID"]), System.Convert.ToInt32(row["Rating"])));
            }

            BusinessTier.MovieDetail newMovieDetail = new BusinessTier.MovieDetail(currMovie, System.Convert.ToDouble(avgRating), System.Convert.ToInt32(numRatings), allReviews);

            return newMovieDetail;              //Returns Moviedetail object
        }
        
        return null;
    }


    //
    // GetUserDetail:
    //
    // Given a USER ID, returns detailed information about this user --- all
    // the reviews submitted by this user, the total number of reviews, average 
    // rating given, etc.  If the user cannot be found, null is returned.
    //
    public UserDetail GetUserDetail(int UserID)
    {
        BusinessTier.User currUser = new BusinessTier.User(UserID);
        //Computes individual user data as far as average rating and number of ratings
        var avgRating = datatier.ExecuteScalarQuery(string.Format(@"SELECT ROUND(AVG(CAST(Rating AS Float)), 2) FROM Reviews WHERE UserID = {0}", UserID));

        var numRatings = datatier.ExecuteScalarQuery(string.Format(@"SELECT COUNT(Rating) FROM Reviews WHERE UserID = {0}", UserID));

        List<Review> allReviews = new List<Review>();
        DataSet ds = datatier.ExecuteNonScalarQuery(string.Format(@"SELECT MovieID, Rating, ReviewID
            FROM Reviews 
            WHERE UserID={0}
            ORDER BY Rating Desc, UserID ASC;",UserID));        //Creates a dataset
        
        DataTable dt = ds.Tables["TABLE"];                  //Creates a datatable from the dataset

        foreach(DataRow row in dt.Rows){
            allReviews.Add(new BusinessTier.Review(System.Convert.ToInt32(row["ReviewID"]), System.Convert.ToInt32(row["MovieID"]), UserID, System.Convert.ToInt32(row["Rating"])));
        }

        BusinessTier.UserDetail userDets = new BusinessTier.UserDetail(currUser, System.Convert.ToDouble(avgRating), System.Convert.ToInt32(numRatings), allReviews);
      
        return userDets;        //Returns userdetail object
    }


    //
    // GetTopMoviesByAvgRating:
    //
    // Returns the top N movies in descending order by average rating.  If two
    // movies have the same rating, the movies are presented in ascending order
    // by name.  If N < 1, an EMPTY LIST is returned.
    //
    public IReadOnlyList<Movie> GetTopMoviesByAvgRating(int N)
    {
      List<Movie> movies = new List<Movie>();
      var count = datatier.ExecuteScalarQuery(string.Format("SELECT COUNT(*) FROM Movies"));

        //If the N input is between 1 and the max number of movies it will run the following
      if (N <= System.Convert.ToInt32(count) && N > 0)
      {
          DataSet ds = datatier.ExecuteNonScalarQuery(string.Format(@"SELECT TOP {0} Movies.MovieName, Movies.MovieID, g.AvgRating 
            FROM Movies
            INNER JOIN 
              (
                SELECT MovieID, ROUND(AVG(CAST(Rating AS Float)), 2) as AvgRating 
                FROM Reviews
                GROUP BY MovieID
              ) g
            ON g.MovieID = Movies.MovieID
            ORDER BY g.AvgRating DESC, Movies.MovieName Asc;",
            N));

          DataTable dt = ds.Tables["TABLE"];        //Creates a datable of the top 10 movies

          foreach (DataRow row in dt.Rows)
          {
              BusinessTier.Movie temp = new BusinessTier.Movie(System.Convert.ToInt32(row["MovieID"]), row["MovieName"].ToString());
              movies.Add(temp);
          }
          //Returns list of movie objects
          return movies;
      }
      return null;      //Otherwise returns null
    }


    //
    // GetTopMoviesByNumReviews
    //
    // Returns the top N movies in descending order by number of reviews.  If two
    // movies have the same number of reviews, the movies are presented in ascending
    // order by name.  If N < 1, an EMPTY LIST is returned.
    //
    public IReadOnlyList<Movie> GetTopMoviesByNumReviews(int N)
    {
      List<Movie> movies = new List<Movie>();

      var count = datatier.ExecuteScalarQuery(string.Format("SELECT COUNT(*) FROM Movies"));
        //Error checking to see if the user entered something greater than 0 but less than or equal to the amount of movies in the database
      if (N <= System.Convert.ToInt32(count) && N > 0)
      {
          DataSet ds = datatier.ExecuteNonScalarQuery(string.Format(@"SELECT TOP {0} Movies.MovieName, Movies.MovieID, g.RatingCount 
            FROM Movies
            INNER JOIN 
              (
                SELECT MovieID, COUNT(*) as RatingCount 
                FROM Reviews
                GROUP BY MovieID
              ) g
            ON g.MovieID = Movies.MovieID
            ORDER BY g.RatingCount DESC, Movies.MovieName Asc;",
            N));

          DataTable dt = ds.Tables["TABLE"];        //Creates a datatable

          foreach (DataRow row in dt.Rows)
          {
              BusinessTier.Movie temp = new BusinessTier.Movie(System.Convert.ToInt32(row["MovieID"]), row["MovieName"].ToString());
              movies.Add(temp);
          }
          //Returns an list of movies
          return movies;
      }
      return null;//Otherwise returns null
    }


    //
    // GetTopUsersByNumReviews
    //
    // Returns the top N users in descending order by number of reviews.  If two
    // users have the same number of reviews, the users are presented in ascending
    // order by user id.  If N < 1, an EMPTY LIST is returned.
    //
    public IReadOnlyList<User> GetTopUsersByNumReviews(int N)
    {
      List<User> users = new List<User>();

      var count = datatier.ExecuteScalarQuery(string.Format("SELECT DISTINCT COUNT(UserID) FROM Reviews"));

        //Error checking if the user entered an input between 1 and the maximum number of users
      if (N <= System.Convert.ToInt32(count) && N > 0)
      {
          DataSet ds = datatier.ExecuteNonScalarQuery(string.Format(@"SELECT TOP {0} UserID, COUNT(*) AS RatingCount
            FROM Reviews
            GROUP BY UserID
            ORDER BY RatingCount DESC, UserID Asc;",
            N));

          DataTable dt = ds.Tables["TABLE"];        //Creates a datatable of the top N users

          foreach (DataRow row in dt.Rows)
          {
              BusinessTier.User temp = new BusinessTier.User(System.Convert.ToInt32(row["UserID"]));
              users.Add(temp);
          }
          //Returns a list of users
          return users;
      }
      return null;  //Otherwise returns null
    }

    public IReadOnlyList<Movie> displayAllMovies()
    {   
        List<Movie> allMovies = new List<Movie>();
        DataSet ds = datatier.ExecuteNonScalarQuery(string.Format("SELECT * FROM Movies ORDER BY MovieName ASC;"));

        DataTable dt = ds.Tables["TABLE"];      //Creates a datatable of all the movies in the database

        foreach (DataRow row in dt.Rows)
        {
            Movie temp = new Movie(System.Convert.ToInt32(row["MovieID"]), row["MovieName"].ToString());
            allMovies.Add(temp);
        }
        return allMovies;               //Returns a list of movie objects
    }

  }//class

}//namespace
