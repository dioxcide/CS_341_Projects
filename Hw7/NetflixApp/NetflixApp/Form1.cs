using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//
// Netflix Database Application using N-Tier Design.
//
// <Antonio Villarreal>
// U. of Illinois, Chicago
// CS341, Fall 2014
// Homework 7
//

namespace NetflixApp
{
	public partial class Form1 : Form
	{
		//
		// Class members:
		//
		private Random RandomNumberGenerator;

		//
		// Constructor:
		//
		public Form1()
		{
			InitializeComponent();

			RandomNumberGenerator = new Random();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

    //
    // Test Connection:
    //
		private void cmdConnect_Click(object sender, EventArgs e)
		{
            var BT = new BusinessTier.Business(this.txtFileName.Text);
            var temp = BT.TestConnection();
            if (temp)   //Tests if the connection is working
            {
                listBox1.Items.Add("Connection Working");
            }
            else
            {
                listBox1.Items.Add("Connection Error");
            }
		}

    //
    // Get Movie Name:  from id...
    //
		private void cmdGetMovieName_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
            if (!string.IsNullOrWhiteSpace(this.txtMovieID.Text))       //Error checking for empty text box
            {
                BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
                var movies = BT.GetMovie(System.Convert.ToInt32(this.txtMovieID.Text));

                if (movies == null)
                {
                    listBox1.Items.Add("Failed To Find Movie");
                }
                else
                {
                    listBox1.Items.Add("Movie Name: " + movies.MovieName);          //If succesful then the movie name is displayed
                }
            }
            else
            {
                listBox1.Items.Add("Failed To Find Movie");
            }
		}

    //
    // Get Movie Reviews: from id...
    //
		private void cmdGetMovieReviews_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

            if (!string.IsNullOrWhiteSpace(this.txtMovieID.Text))           //Error checks for whitespace 
            {
                BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
                var movies = BT.GetMovieDetail(System.Convert.ToInt32(this.txtMovieID.Text));       //Runs the following command

                if (movies == null)                                         //Checks to see if the movie is actually in the database
                {
                    listBox1.Items.Add("Failed To Find Movie");
                }
                else
                {
                    IReadOnlyCollection<BusinessTier.Review> reviews = movies.Reviews;

                    foreach (var temp in reviews)
                    {
                        listBox1.Items.Add(temp.UserID.ToString() + ": " + temp.Rating.ToString());     //Displays all the reviews of the movie from each user
                    }
                }
            }
            else
            {
                listBox1.Items.Add("Failed To Find Movie");             
            }
            
		}

		//
		// Average Rating:
		//
		private void cmdAvgRating_Click(object sender, EventArgs e)
		{
            listBox1.Items.Clear();

            if (!string.IsNullOrWhiteSpace(this.txtRatingsMovieName.Text))  //Error checking for whitespace in the text box
            {
                BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
                var m1 = BT.GetMovie(this.txtRatingsMovieName.Text);            //Runs the following command


                if (m1 == null)                                                 //Checks if the movie is in the database o
                {
                    listBox1.Items.Add("Movie Not Found");
                }
                else
                {                                                               //Displays average rating for the movie
                    var m2 = BT.GetMovieDetail(m1.MovieID);
                    listBox1.Items.Add("Movie Name: " + m2.movie.MovieName);
                    listBox1.Items.Add("Average Rating: " + m2.AvgRating.ToString());
                }
            }
            else
            {
                listBox1.Items.Add("Movie Not Specified"); 
            }
		}

		//
		// Each Rating:
		//
		private void cmdEachRating_Click(object sender, EventArgs e)
		{
            listBox1.Items.Clear();

            if (!string.IsNullOrWhiteSpace(this.txtRatingsMovieName.Text)) ///Error checking for whitespace
            {
                BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
                var movieID = BT.GetMovie(this.txtRatingsMovieName.Text);           //Runs the following commands
                var movies = BT.GetMovieDetail(movieID.MovieID);

                if (movies == null)                 //Detects if the movie is in the databse or not
                {
                    listBox1.Items.Add("Failed To Find Movie");
                }
                else
                {
                    int one, two, three, four, five;            //variables to store each rating
                    one = two = three = four = five = 0;

                    IReadOnlyCollection<BusinessTier.Review> reviews = movies.Reviews;

                    one = reviews.Where(temp => temp.Rating == 1).Count();      //Counts each rating
                    two = reviews.Where(temp => temp.Rating == 2).Count();
                    three = reviews.Where(temp => temp.Rating == 3).Count();
                    four = reviews.Where(temp => temp.Rating == 4).Count();
                    five = reviews.Where(temp => temp.Rating == 5).Count();

                    listBox1.Items.Add(movies.movie.MovieName);             //Displays the ratings to the user
                    listBox1.Items.Add("5:" + five.ToString());
                    listBox1.Items.Add("4:" + four.ToString());
                    listBox1.Items.Add("3:" + three.ToString());
                    listBox1.Items.Add("2:" + two.ToString());
                    listBox1.Items.Add("1:" + one.ToString());
                    listBox1.Items.Add("Total Reviews:" + movies.NumReviews);
                }
            }
            else
            {
                listBox1.Items.Add("Movie Not Specified");
            }
		}
	
		//
		// Add movie:
		//
		private void cmdInsertMovie_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

            if (!string.IsNullOrWhiteSpace(this.txtInsertMovieName.Text))//Error checks for whitespace
            {
                BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
                BusinessTier.Movie movie = BT.AddMovie(txtInsertMovieName.Text);                //Attemps to add the movie

                if (movie == null)                  //Detects if the movie is already in the database
                {
                    listBox1.Items.Add("Failure, movie not added.");
                }
                else
                {                                                                       //Otherwise it adds the movie to the database
                    listBox1.Items.Add("** Insert Success! **");
                    listBox1.Items.Add("Movie ID: " + movie.MovieID.ToString());
                }
            }
            else
            {
                listBox1.Items.Add("Movie Not Specified");
            }
		}

		private void tbarRating_Scroll(object sender, EventArgs e)
		{
			lblRating.Text = tbarRating.Value.ToString();
		}

		//
		// Add Review:
		//
		private void cmdInsertReview_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
            if (!string.IsNullOrWhiteSpace(this.txtInsertMovieName.Text))       //Checks for whitespace in textbox
            {
                BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
                var m1 = BT.GetMovie(this.txtInsertMovieName.Text);            //Attempts to get the movie

                if (m1 == null)
                {
                    listBox1.Items.Add("Movie Not Found");          //If the movie is not found in the database then the following is displayed
                }
                else
                {
                    var rev = BT.AddReview(m1.MovieID, 1337, System.Convert.ToInt32(tbarRating.Value.ToString()));  //Otherwise it displays the following
                    listBox1.Items.Add("Review Added");
                    listBox1.Items.Add("Revied ID: " + rev.ReviewID);
                }
            }
            else
            {
                listBox1.Items.Add("Movie Not Specified");
            }
		}

    //
    // Top N Movies by Average Rating:
    //
    private void cmdTopMoviesByAvgRating_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();

      if (!string.IsNullOrWhiteSpace(this.txtTopN.Text))        //Error checking for whitespace in the textbox
      {
          BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
          IReadOnlyList<BusinessTier.Movie> movies = BT.GetTopMoviesByAvgRating(System.Convert.ToInt32(txtTopN.Text));      //Executes the command for the TOP n movies

          if (movies == null)
          {
              listBox1.Items.Add("Failed To Display Top N Movies"); //If the N value is out of range it will display the following
          }
          else
          {
              foreach (BusinessTier.Movie movie in movies)
              {
                  BusinessTier.MovieDetail detail = BT.GetMovieDetail(movie.MovieID);       //Displays the top N movies
                  this.listBox1.Items.Add(movie.MovieName +": "+detail.AvgRating);
              }
          }
      }
      else
      {
          listBox1.Items.Add("Movie Not Specified");
      }
    }

    //
    // Top N Movies by # of reviews:
    //
    private void cmdTopMoviesByNumReviews_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();

      if (!string.IsNullOrWhiteSpace(this.txtTopN.Text))        //Error checking for whitespace in the textbox
      {
          BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
          IReadOnlyList<BusinessTier.Movie> movies = BT.GetTopMoviesByNumReviews(System.Convert.ToInt32(txtTopN.Text));     //Executes the command for top N movies

          if (movies == null)           //Detects to see if the N value is in range
          {
              listBox1.Items.Add("Failed To Display Top N Movies");
          }
          else
          {
              foreach (BusinessTier.Movie movie in movies)
              {
                  var detail = BT.GetMovieDetail(movie.MovieID);                        //Displays the Top N movies to the user
                  this.listBox1.Items.Add(movie.MovieName+": "+detail.NumReviews);
              }
          }
      }
      else
      {
          listBox1.Items.Add("Movie Not Specified");
      }
    }

    //
    // Top N Users by # of reviews:
    //
    private void cmdTopUsers_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();
            
      if (!string.IsNullOrWhiteSpace(this.txtTopN.Text))            //Error checking for whitespace in textbox
      {
          BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
          IReadOnlyList<BusinessTier.User> users = BT.GetTopUsersByNumReviews(System.Convert.ToInt32(txtTopN.Text));        //Getss top N users

          if (users == null)
          {
              listBox1.Items.Add("Failed To Display Top N Movies");         //If N is out of range it will display the following
          }
          else
          {
              foreach (var temp in users)
              {
                  BusinessTier.UserDetail tempUser = BT.GetUserDetail(temp.UserID);         //Displays the Top N users
                  this.listBox1.Items.Add(temp.UserID.ToString()+":"+tempUser.NumReviews);
              }

          }
      }
      else
      {
          listBox1.Items.Add("Movie Not Specified");
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        listBox1.Items.Clear();

        BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
        IReadOnlyList<BusinessTier.Movie> allMovies = BT.displayAllMovies();

        if (allMovies != null)
        {
            listBox1.Items.Add("Movies".PadRight(40) + "ID");
            foreach (var temp in allMovies)
            {
                listBox1.Items.Add(temp.MovieName.PadRight(40) + temp.MovieID.ToString());      //Displays all movies in alphabetical order
            }
        }
        else
        {
            listBox1.Items.Add("No movies to be displayed!");                   //If the database is empty the following is displayed
        }
    }

    private void txtInsertMovieName_TextChanged(object sender, EventArgs e)
    {

    }

    private void txtTopN_TextChanged(object sender, EventArgs e)
    {

    }

    private void txtMovieID_TextChanged(object sender, EventArgs e)
    {

    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

	}//class
}//namespace
