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

//
// Netflix Database Application.
//
// <<Antonio Villarreal>>
// U. of Illinois, Chicago
// CS341, Fall 2014
// Homework 6
//

namespace HW6
{
    public partial class Form1 : Form
    {
        string filename, connectionInfo;
        SqlConnection dbConn;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                 
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //Adds a new movie to the database
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                //Opens the SQL Connection to the netflix DataBase
                filename = "netflix.mdf";
                connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);
                dbConn = new SqlConnection(connectionInfo);     //New Connection created
                dbConn.Open();                                  //Opened

                string sql, msg, temp;
                SqlCommand dbCmd;
                object result;

                //Takes into account if the Movie Title has a '
                temp = this.textBox1.Text.Replace("'", "''");
                sql = "insert into Movies (MovieName) values ('" + temp + "');";  //SQL Statement to add the new movie to the database
                dbCmd = new SqlCommand();                                       //New sql command
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                result = dbCmd.ExecuteScalar();                                 //Executes the Command

                //Displays in the box in the interface that the movie has been added
                msg = String.Format("Movie {0} has been added ", this.textBox1.Text);
                this.richTextBox1.AppendText(msg + Environment.NewLine);
                dbConn.Close();                                                 //Closes connection
            }
            else
            {
                this.richTextBox1.AppendText("No Movie Specified" + Environment.NewLine);
            }
        }

        //Adds a new review to an existing movie
        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox1.Text) || !string.IsNullOrWhiteSpace(this.textBox2.Text))
            {
                //Opens a new connection to the database 
                filename = "netflix.mdf";
                connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);
                dbConn = new SqlConnection(connectionInfo);
                dbConn.Open();                              //Opens a new connection

                string sql, msg;
                SqlCommand dbCmd;
                object result;
                
                //Sql command to be executed
                sql = "insert into Reviews (MovieID,UserID,Rating) values (" + "(select MovieID from Movies where MovieName = '" + this.textBox1.Text + "')" + ",1337," + this.textBox2.Text + ");";
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                result = dbCmd.ExecuteScalar();

                //Appends the following message to the textbox to let the user know the rating has been added
                msg = String.Format("Movie {0} has been rated {1} ", this.textBox1.Text, this.textBox2.Text);
                this.richTextBox1.AppendText(msg + Environment.NewLine);

                dbConn.Close();         //Closes stream
            }
            else
            {   //Alerts the user that they forgot to add a rating or movie name
                this.richTextBox1.AppendText("No Movie Specified or Rating" + Environment.NewLine);
            }
        }
        //Displays the average rating for a movie
        private void button3_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(this.textBox1.Text) )
            {
                //Opens a new connection to the database
                filename = "netflix.mdf";
                connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);
                dbConn = new SqlConnection(connectionInfo);
                dbConn.Open();

                string sql, msg;
                SqlCommand dbCmd;
                object result;

                //Sql command to be executed
                sql = "SELECT ROUND(AVG(CAST(Rating AS float)), 2) FROM Reviews WHERE MovieID = (select MovieID from Movies where MovieName = '" + this.textBox1.Text + "');";
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                result = dbCmd.ExecuteScalar();

                //Prints out a message to the user telling them that the rating has been added
                msg = String.Format("Average Rating for {0}: {1}", this.textBox1.Text, result);
                this.richTextBox1.AppendText(msg + Environment.NewLine);

                dbConn.Close(); //Stream closses
            }
            else
            {
                this.richTextBox1.AppendText("Movie text box blank" + Environment.NewLine);
            }
        }

        //Displays each individual 5,4,3,2,1 ratings for the movie given
        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox1.Text))
            {
                //Opens the connection to the database
                filename = "netflix.mdf";
                connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);
                dbConn = new SqlConnection(connectionInfo);
                dbConn.Open();      //Opens connection

                string sql, msg;
                SqlCommand dbCmd;
                object result, totalReviews, one, two, three, four, five;

                //Executes the following sql commands to get 1, 2, 3, 4, 5 star ratings and total number of reviews 

                sql = "select count(Rating) from Reviews where MovieID= (SELECT MovieID from Movies where MovieName = '" + this.textBox1.Text + "') and Rating = 1;";
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                one = dbCmd.ExecuteScalar();

                sql = "select count(Rating) from Reviews where MovieID= (SELECT MovieID from Movies where MovieName = '" + this.textBox1.Text + "') and Rating = 2;";
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                two = dbCmd.ExecuteScalar();

                sql = "select count(Rating) from Reviews where MovieID= (SELECT MovieID from Movies where MovieName = '" + this.textBox1.Text + "') and Rating = 3;";
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                three = dbCmd.ExecuteScalar();

                sql = "select count(Rating) from Reviews where MovieID= (SELECT MovieID from Movies where MovieName = '" + this.textBox1.Text + "') and Rating = 4;";
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                four = dbCmd.ExecuteScalar();

                sql = "select count(Rating) from Reviews where MovieID= (SELECT MovieID from Movies where MovieName = '" + this.textBox1.Text + "') and Rating = 5;";
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                five = dbCmd.ExecuteScalar();

                sql = "select count(Rating) from Reviews where MovieID= (SELECT MovieID from Movies where MovieName = '" + this.textBox1.Text + "');";
                dbCmd = new SqlCommand();
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                totalReviews = dbCmd.ExecuteScalar();

                //Displays to the user the number of 1, 2, 3, 4, 5, total ratings of the specified movie
                msg = String.Format("Movie {0} Reviews" + Environment.NewLine + "5: {1}" + Environment.NewLine + "4: {2}"
                    + Environment.NewLine + "3: {3}" + Environment.NewLine + "2: {4}"
                    + Environment.NewLine + "1: {5}"
                    + Environment.NewLine + "Total Reviews: {6}", this.textBox1.Text, five, four, three, two, one, totalReviews.ToString());
                this.richTextBox1.AppendText(msg + Environment.NewLine);

                dbConn.Close();//Closes stream
            }
            else
            {
                this.richTextBox1.AppendText("Movie text box is blank" + Environment.NewLine);
            }
        }

        //Top N Movies by Average Rating
        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox3.Text))
            {
                //Opens a connection to the netflix database
                filename = "netflix.mdf";
                connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);
                dbConn = new SqlConnection(connectionInfo);
                dbConn.Open();              //Opens the connetion

                string sql, msg;
                SqlCommand dbCmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(dbCmd);
                DataSet ds = new DataSet();
                
                //Runs SQL Command and stores it into a temporary data set 
                sql = "SELECT TOP " + this.textBox3.Text + " MovieName, AvgRating FROM Movies INNER JOIN (SELECT MovieID, ROUND(AVG(CAST(Rating AS Float)), 2) as AvgRating FROM Reviews GROUP BY MovieID) TEMP ON TEMP.MovieID = Movies.MovieID ORDER BY AvgRating DESC, MovieName ASC;";
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                adapter.Fill(ds);

                //Gets the dataset and sets it up as a table
                DataTable dt = ds.Tables["TABLE"];

                //Prints out the table data to the user
                this.richTextBox1.AppendText("Top N Movies by Average Rating"+Environment.NewLine+"MovieName\t\t\tAverage Rating"+Environment.NewLine);

                foreach (DataRow row in dt.Rows)
                {   //Prints the moviename and avg rating
                    msg = row["MovieName"] + ":\t\t\t" + row["AvgRating"].ToString() + Environment.NewLine;

                    this.richTextBox1.AppendText(msg + Environment.NewLine);
                }

                dbConn.Close();
            }
            else
            {
                this.richTextBox1.AppendText("Number of N movies not specified in the textbox"+Environment.NewLine);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox3.Text) )
            {
                //Opening a connection to the netflix database
                filename = "netflix.mdf";
                connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);
                dbConn = new SqlConnection(connectionInfo);
                dbConn.Open();                              //Opening connection

                string sql, msg;
                SqlCommand dbCmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(dbCmd);
                DataSet ds = new DataSet();

                //Runs the following SQL command and stores the output into a data set
                sql = "SELECT DISTINCT TOP " + this.textBox3.Text + " Reviews.UserID, TotalReviews FROM Reviews INNER JOIN (SELECT DISTINCT UserID, COUNT(UserID) as TotalReviews FROM Reviews GROUP BY UserID) TEMP ON TEMP.UserID = Reviews.UserID ORDER BY TotalReviews DESC, UserID ASC;"; 
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                adapter.Fill(ds);

                //Converts the data set into a table
                DataTable dt = ds.Tables["TABLE"];

                //Displays to the user the Top N users in the database
                this.richTextBox1.AppendText("Top N Users"+Environment.NewLine+"Users\tTotal Reviews" + Environment.NewLine);
   
                foreach (DataRow row in dt.Rows)
                {
                    msg = row["UserID"] + ":\t" + row["TotalReviews"].ToString() + Environment.NewLine;

                    this.richTextBox1.AppendText(msg + Environment.NewLine);
                }

                dbConn.Close(); //Closes stream
            }
            else
            {
                this.richTextBox1.AppendText("Top N Users not specified in textbox"+Environment.NewLine);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBox3.Text))
            {
                //Opens a connection stream to the netflix database
                filename = "netflix.mdf";
                connectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", filename);
                dbConn = new SqlConnection(connectionInfo);
                dbConn.Open();                              //Opens a connection

                string sql, msg;
                SqlCommand dbCmd = new SqlCommand();
                SqlDataAdapter adapter = new SqlDataAdapter(dbCmd);
                DataSet ds = new DataSet();

                //SQL statement that will be executed
                sql = "SELECT DISTINCT TOP " + this.textBox3.Text + " MovieName, TotalReviews FROM Movies INNER JOIN (SELECT DISTINCT MovieID, COUNT(MovieID) as TotalReviews FROM Reviews GROUP BY MovieID) TEMP ON TEMP.MovieID = Movies.MovieID ORDER BY TotalReviews DESC, MovieName ASC;";
                dbCmd.Connection = dbConn;
                dbCmd.CommandText = sql;
                adapter.Fill(ds);

                //Creates a table out of the dataset we got from the SQL statement
                DataTable dt = ds.Tables["TABLE"];

                //Displays to the user the top N movies by total reviews
                this.richTextBox1.AppendText("Top N Movies by Total Reviews"+Environment.NewLine+"Movies\t\t\tTotal Reviews" + Environment.NewLine);
                
                foreach (DataRow row in dt.Rows)
                {
                    msg = row["MovieName"] + ":\t\t\t" + row["TotalReviews"].ToString() + Environment.NewLine;

                    this.richTextBox1.AppendText(msg + Environment.NewLine);
                }

                dbConn.Close();
            }
            else
            {
                this.richTextBox1.AppendText("Top N Movies not specified in the textbox" + Environment.NewLine);
            }
        }

        
    }
}
