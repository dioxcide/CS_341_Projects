//
// Data Access Tier:  interface between business tier and data store.
//

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DataAccessTier
{

  public class Data
  {
    //
    // Fields:
    //
    private string _DBFile;
    private string _DBConnectionInfo;

    //
    // constructor:
    //
    public Data(string DatabaseFilename)
    {
      _DBFile = DatabaseFilename;
      this._DBConnectionInfo = String.Format(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\{0};Integrated Security=True;", DatabaseFilename);
    }

    //
    // TestConnection:  returns true if the database can be successfully opened and closed,
    // false if not.
    //
    public bool TestConnection()
    {
      SqlConnection db = new SqlConnection(this._DBConnectionInfo);

      bool  state = false;

      try
      {                         //Try catch statement testing out the connection to see if it fails or connects
        db.Open();

        state = (db.State == ConnectionState.Open);
      }
      catch
      {
        // nothing, just discard:
      }
      finally
      {
        db.Close();
      }

      return state;
    }

    //
    // ExecuteScalarQuery:  executes a scalar Select query, returning the single result 
    // as an object.  
    //
    public object ExecuteScalarQuery(string sql)
    {
        SqlConnection db = new SqlConnection(this._DBConnectionInfo);
        SqlCommand cmd = new SqlCommand();              //Sets up the connection to the database

        db.Open();
        cmd.Connection = db;
        cmd.CommandText = sql;

        object result = cmd.ExecuteScalar();        //Executes the scalar command
        db.Close();
      
      return result;
    }

    // 
    // ExecuteNonScalarQuery:  executes a Select query that generates a temporary table,
    // returning this table in the form of a Dataset.
    //
    public DataSet ExecuteNonScalarQuery(string sql)
    {
        SqlConnection db;

        db = new SqlConnection(this._DBConnectionInfo);         ///Sets up the connection to the server 
        db.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;
        cmd.CommandText = sql;

        SqlDataAdapter adapter = new SqlDataAdapter(cmd);       //Creates a data set out of the data
        DataSet ds = new DataSet();

        adapter.Fill(ds);  // execute!                  //Fills the table

        db.Close();
      
      return ds;
    }

    //
    // ExecutionActionQuery:  executes an Insert, Update or Delete query, and returns
    // the number of records modified.
    //
    public int ExecuteActionQuery(string sql)
    {
        SqlConnection db;

        db = new SqlConnection(this._DBConnectionInfo);         //Opens the connection to the database
        db.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = db;

        //
        // insert movie:
        //
        cmd.CommandText = sql;

        int temp = cmd.ExecuteNonQuery();               //Executes the action query

      return temp;
    }

  }//class

}//namespace
