using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
            if (temp)
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

            BusinessTier.Business BT = new BusinessTier.Business(this.txtFileName.Text);
            var movies = BT.GetMovie(System.Convert.ToInt32(this.txtMovieID.Text));

            if (movies == null)
            {
                listBox1.Items.Add("Failed To Find Movie");
            }
            else
            {
                listBox1.Items.Add("Movie Name: " + movies.MovieName);
            }
		}

    //
    // Get Movie Reviews: from id...
    //
		private void cmdGetMovieReviews_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

            BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
            var movies = BT.GetMovieDetail(System.Convert.ToInt32(this.txtMovieID.Text));

            if (movies ==  null)
            {
                listBox1.Items.Add("Failed To Find Movie");
            }
            else
            {
                int one, two, three, four, five;
                one = two = three = four = five = 0;

                IReadOnlyCollection<BusinessTier.Review> reviews = movies.Reviews;

                foreach (var temp in reviews)
                {
                    listBox1.Items.Add(temp.UserID.ToString() + ": " + temp.Rating.ToString());
                }
            }
            
		}

		//
		// Average Rating:
		//
		private void cmdAvgRating_Click(object sender, EventArgs e)
		{
            listBox1.Items.Clear();

            BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
            var m1 = BT.GetMovie(this.txtRatingsMovieName.Text);
            

            if (m1 == null)
            {
                listBox1.Items.Add("Movie Not Found");
            }
            else{
                var m2 = BT.GetMovieDetail(m1.MovieID);
                listBox1.Items.Add("Movie Name: "+m2.movie.MovieName);
                listBox1.Items.Add("Average Rating: "+m2.AvgRating.ToString());
            }
		}

		//
		// Each Rating:
		//
		private void cmdEachRating_Click(object sender, EventArgs e)
		{
            listBox1.Items.Clear();

            BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
            var movies = BT.GetMovieDetail(System.Convert.ToInt32(this.txtMovieID.Text));

            if (movies == null)
            {
                listBox1.Items.Add("Failed To Movie");
            }
            else
            {
                int one, two, three, four, five;
                one = two = three = four = five = 0;

                IReadOnlyCollection<BusinessTier.Review> reviews = movies.Reviews;

                one = reviews.Where(temp => temp.Rating == 1).Count();
                two = reviews.Where(temp => temp.Rating == 2).Count();
                three = reviews.Where(temp => temp.Rating == 3).Count();
                four = reviews.Where(temp => temp.Rating == 4).Count();
                five = reviews.Where(temp => temp.Rating == 5).Count();

                listBox1.Items.Add(movies.movie.MovieName);
                listBox1.Items.Add("5:" + five.ToString());
                listBox1.Items.Add("4:" + four.ToString());
                listBox1.Items.Add("3:" + three.ToString());
                listBox1.Items.Add("2:" + two.ToString());
                listBox1.Items.Add("1:" + one.ToString());
                listBox1.Items.Add("Total Reviews:" + movies.NumReviews);
            }
		}
	
		//
		// Add movie:
		//
		private void cmdInsertMovie_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

            BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
            BusinessTier.Movie movie = BT.AddMovie(txtInsertMovieName.Text);

			if (movie == null) // success!
			{
				listBox1.Items.Add("Failure, movie not added.");
			}
			else
			{
				listBox1.Items.Add("** Insert Success! **");
                listBox1.Items.Add("Movie ID: " + movie.MovieID.ToString());
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
           
            BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
            var m1 = BT.GetMovie(this.txtInsertMovieName.Text);

            if (m1 == null)
            {
                listBox1.Items.Add("Movie Not Found");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("MovieID: "+m1.MovieID.ToString());
                var rev = BT.AddReview(m1.MovieID, 1337, System.Convert.ToInt32(tbarRating.Value.ToString()));
                listBox1.Items.Add("Review Added");
                listBox1.Items.Add("Revied ID: " + rev.ReviewID);
            }
		}

    //
    // Top N Movies by Average Rating:
    //
    private void cmdTopMoviesByAvgRating_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();
        
        BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
        IReadOnlyList<BusinessTier.Movie> movies = BT.GetTopMoviesByAvgRating(System.Convert.ToInt32(txtTopN.Text));

        if (movies == null)
        {
            listBox1.Items.Add("Failed To Display Top N Movies");
        }
        else
        {
            System.Windows.Forms.MessageBox.Show("My message here");
            foreach(BusinessTier.Movie movie in movies){
                BusinessTier.MovieDetail detail = BT.GetMovieDetail(movie.MovieID);
                this.listBox1.Items.Add(movie.MovieName);
                this.listBox1.Items.Add(detail.AvgRating);
            }
        }
    }

    //
    // Top N Movies by # of reviews:
    //
    private void cmdTopMoviesByNumReviews_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();
     
      BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
      IReadOnlyList<BusinessTier.Movie> movies = BT.GetTopMoviesByNumReviews(System.Convert.ToInt32(txtTopN.Text));

      if (movies == null)
      {
          listBox1.Items.Add("Failed To Display Top N Movies");
      }
      else
      {
          foreach (BusinessTier.Movie movie in movies)
          {
              var detail = BT.GetMovieDetail(movie.MovieID);
              this.listBox1.Items.Add(movie.MovieName);
              this.listBox1.Items.Add(detail.NumReviews);
          }
      }
    }

    //
    // Top N Users by # of reviews:
    //
    private void cmdTopUsers_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();

      BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
      IReadOnlyList<BusinessTier.User> users = BT.GetTopUsersByNumReviews(System.Convert.ToInt32(txtTopN.Text));

      if (users == null)
      {
          listBox1.Items.Add("Failed To Display Top N Movies");
      }
      else
      {
          foreach (var temp in users)
          {
              //var detail = BT.GetMovieDetail(temp.MovieID);
              this.listBox1.Items.Add(temp.UserID.ToString());
              //this.listBox1.Items.Add(t);
          }
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        BusinessTier.Business BT = new BusinessTier.Business("netflix.mdf");
        IReadOnlyList<BusinessTier.Movie> allMovies = BT.displayAllMovies();

        listBox1.Items.Add("Movies".PadRight(20)+"ID");
        foreach (var temp in allMovies)
        {
            listBox1.Items.Add(temp.MovieName+ "".PadRight(20) + temp.MovieID.ToString());
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
