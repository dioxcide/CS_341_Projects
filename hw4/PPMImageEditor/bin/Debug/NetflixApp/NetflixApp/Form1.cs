using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


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
      try
      {
        string filename, connectionInfo;
        SqlConnection db;

        filename = this.txtFileName.Text;
        connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

        db = new SqlConnection(connectionInfo);
        db.Open();

        string msg = db.State.ToString();
        MessageBox.Show(msg);

        db.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error: " + ex.Message);
      }
		}

    //
    // Get Movie Name:  from id...
    //
		private void cmdGetMovieName_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = db;

      cmd.CommandText = string.Format("SELECT MovieName FROM Movies WHERE MovieID={0};",
        txtMovieID.Text);

      //MessageBox.Show(cmd.CommandText);

			object result = cmd.ExecuteScalar();

			db.Close();

      if (result == null || result.ToString() == "")
      {
        listBox1.Items.Add("Movie not found...");
      }
      else
      {
        listBox1.Items.Add(result.ToString());
      }
		}

    //
    // Get Movie Reviews: from id...
    //
		private void cmdGetMovieReviews_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = db;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();

      cmd.CommandText = "SELECT UserID, Rating FROM Reviews WHERE MovieID = " + txtMovieID.Text + ";";
      cmd.CommandText = string.Format(@"SELECT UserID, Rating 
            FROM Reviews 
            WHERE MovieID={0}
            ORDER BY Rating Desc, UserID ASC;",
        txtMovieID.Text);

      //MessageBox.Show(cmd.CommandText);

			adapter.Fill(ds);

			db.Close();

			DataTable dt = ds.Tables["TABLE"];

      if (dt.Rows.Count == 0)
      {
        listBox1.Items.Add("Movie not found, or no reviews...");
      }
      else
      {
        foreach (DataRow row in dt.Rows)
          listBox1.Items.Add(row["UserID"] + ": " + row["Rating"]);
      }
		}

		//
		// Average Rating:
		//
		private void cmdAvgRating_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

			//
			// Compute average rating:
			//
			string name = txtRatingsMovieName.Text;
			name = name.Replace("'", "''");  // escape any single ' in the string:

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = db;

			cmd.CommandText = string.Format(@"SELECT ROUND(AVG(CAST(Rating AS Float)), 2) AS AvgRating 
          FROM Reviews
					INNER JOIN Movies ON Reviews.MovieID = Movies.MovieID
					WHERE Movies.MovieName='{0}';", 
        name);

			//MessageBox.Show(cmd.CommandText);

			object result = cmd.ExecuteScalar();

			db.Close();

			if (result == null || result.ToString() == "")
			{
				listBox1.Items.Add("Movie not found...");
			}
			else
			{
				listBox1.Items.Add("Average rating: " + result.ToString());
			}
		}

		//
		// Each Rating:
		//
		private void cmdEachRating_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = db;

			//
			// Group all the ratings for given movie, and then count each group:
			//
			string name = txtRatingsMovieName.Text;
			name = name.Replace("'", "''");  // escape any single ' in the string:

			cmd.CommandText = string.Format(@"SELECT Rating, COUNT(Rating) as RatingCount
          FROM Reviews
					INNER JOIN Movies ON Reviews.MovieID = Movies.MovieID
					WHERE Movies.MovieName='{0}'
          GROUP BY Rating
          ORDER BY Rating DESC;",
				name);

			//MessageBox.Show(cmd.CommandText);

			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();

			adapter.Fill(ds);  // execute!

			db.Close();

			//
			// display results:
			//
			int total = 0;

			DataTable dt = ds.Tables["TABLE"];

			if (dt.Rows.Count == 0)
			{
				listBox1.Items.Add("Movie not found...");
			}
			else
			{
				//
				// we have ratings data, display:
				//
				foreach (DataRow row in dt.Rows)
				{
					listBox1.Items.Add(row["Rating"] + ": " + row["RatingCount"]);

					total = total + Convert.ToInt32(row["RatingCount"]);
				}

				listBox1.Items.Add("Total: " + total.ToString());
			}
		}
	
		//
		// Add movie:
		//
		private void cmdInsertMovie_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = db;
			
			//
			// insert movie:
			//
			string name = txtInsertMovieName.Text;
			name = name.Replace("'", "''");  // escape any single ' in the string:

			cmd.CommandText = string.Format("INSERT INTO Movies(MovieName) Values('{0}');",
				name);

			//MessageBox.Show(cmd.CommandText);

			int rowsModified = cmd.ExecuteNonQuery();

			//
			// display results:
			//
			listBox1.Items.Add("Rows modified: " + rowsModified.ToString());

			if (rowsModified == 1) // success!
			{
				listBox1.Items.Add("Success, movie added.");
			}
			else
			{
				listBox1.Items.Add("** Insert failed?! **");
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

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = db;

			//
			// first we need to lookup the movie id:
			//
			string name = txtInsertMovieName.Text;
			name = name.Replace("'", "''");  // escape any single ' in the string:

			cmd.CommandText = string.Format("SELECT MovieID FROM Movies WHERE MovieName='{0}';",
				name);

			//MessageBox.Show(cmd.CommandText);
			
			object result = cmd.ExecuteScalar();

			if (result == null || result.ToString() == "")
			{
				listBox1.Items.Add("Movie not found...");
			}
			else
			{
				//
				// Found movie, so now we can insert movie review:
				//
				int movieid = Convert.ToInt32(result);
				int userid = RandomNumberGenerator.Next(100000, 999999);  // 6-digit user ids:

				//
				// now insert review:
				//
				cmd.CommandText = string.Format("INSERT INTO Reviews(MovieID, UserID, Rating) Values({0}, {1}, {2});",
					movieid,
					userid,
					lblRating.Text);

				//MessageBox.Show(cmd.CommandText);

				int rowsModified = cmd.ExecuteNonQuery();

				//
				// display results:
				//
				listBox1.Items.Add("Rows modified: " + rowsModified.ToString());

				if (rowsModified == 1) // success!
				{
					listBox1.Items.Add("Success, review added.");
				}
				else
				{
					listBox1.Items.Add("** Insert failed?! **");
				}
			}
		}

    //
    // Top N Movies by Average Rating:
    //
    private void cmdTopMoviesByAvgRating_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;

      //
      // Group all the reviews for each movie, compute averages, and take top N:
      //
      string N = txtTopN.Text;

      cmd.CommandText = string.Format(@"SELECT TOP {0} Movies.MovieName, g.AvgRating 
            FROM Movies
            INNER JOIN 
              (
                SELECT MovieID, ROUND(AVG(CAST(Rating AS Float)), 2) as AvgRating 
                FROM Reviews
                GROUP BY MovieID
              ) g
            ON g.MovieID = Movies.MovieID
            ORDER BY g.AvgRating DESC, Movies.MovieName Asc;",
        N);

      //MessageBox.Show(cmd.CommandText);

      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      adapter.Fill(ds);  // execute!

      db.Close();

      //
      // display results:
      //
      DataTable dt = ds.Tables["TABLE"];

      if (dt.Rows.Count == 0)
      {
        listBox1.Items.Add("**Error, or database is empty?!");
      }
      else
      {
        //
        // we have ratings data, display:
        //
        foreach (DataRow row in dt.Rows)
          listBox1.Items.Add(row["MovieName"] + ": " + row["AvgRating"]);
      }
    }

    //
    // Top N Movies by # of reviews:
    //
    private void cmdTopMoviesByNumReviews_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;

      //
      // Group all the reviews for each movie, compute averages, and take top N:
      //
      string N = txtTopN.Text;

      cmd.CommandText = string.Format(@"SELECT TOP {0} Movies.MovieName, g.RatingCount 
            FROM Movies
            INNER JOIN 
              (
                SELECT MovieID, COUNT(*) as RatingCount 
                FROM Reviews
                GROUP BY MovieID
              ) g
            ON g.MovieID = Movies.MovieID
            ORDER BY g.RatingCount DESC, Movies.MovieName Asc;",
        N);

      //MessageBox.Show(cmd.CommandText);

      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      adapter.Fill(ds);  // execute!

      db.Close();

      //
      // display results:
      //
      DataTable dt = ds.Tables["TABLE"];

      if (dt.Rows.Count == 0)
      {
        listBox1.Items.Add("**Error, or database is empty?!");
      }
      else
      {
        //
        // we have ratings data, display:
        //
        foreach (DataRow row in dt.Rows)
          listBox1.Items.Add(row["MovieName"] + ": " + row["RatingCount"]);
      }
    }

    //
    // Top N Users by # of reviews:
    //
    private void cmdTopUsers_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();

      string filename, connectionInfo;
      SqlConnection db;

      filename = this.txtFileName.Text;
      connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);

      db = new SqlConnection(connectionInfo);
      db.Open();

      SqlCommand cmd = new SqlCommand();
      cmd.Connection = db;

      //
      // Group all the reivews by user, count, and take top N:
      //
      string N = txtTopN.Text;

      cmd.CommandText = string.Format(@"SELECT TOP {0} UserID, COUNT(*) AS RatingCount
            FROM Reviews
            GROUP BY UserID
            ORDER BY RatingCount DESC, UserID Asc;",
        N);

      //MessageBox.Show(cmd.CommandText);

      SqlDataAdapter adapter = new SqlDataAdapter(cmd);
      DataSet ds = new DataSet();

      adapter.Fill(ds);  // execute!

      db.Close();

      //
      // display results:
      //
      DataTable dt = ds.Tables["TABLE"];

      if (dt.Rows.Count == 0)
      {
        listBox1.Items.Add("**Error, or database is empty?!");
      }
      else
      {
        //
        // we have ratings data, display:
        //
        foreach (DataRow row in dt.Rows)
          listBox1.Items.Add(row["UserID"] + ": " + row["RatingCount"]);
      }
    }

	}//class
}//namespace
