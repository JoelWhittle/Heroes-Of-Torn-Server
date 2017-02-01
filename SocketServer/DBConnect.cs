using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;


    public static class DBConnect
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;

        //Constructor
        static DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private static void Initialize()
        {
            server = "localhost";
            database = "test";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public static void Insert(string query)
        {
         //   string query = "INSERT INTO test_table (name, gender,food) VALUES('John Smith', 'confused', 'please')";

            //open connection
            if (OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        //Update statement
        public static void Update(string query)
        {
          //  string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
               CloseConnection();
            }
        }

        //Delete statement
        public static void Delete(string query)
        {
          //  string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
               CloseConnection();
            }
        }

        //Select statement
        public static string Select(string query)
        {
        //    string query = "SELECT * FROM tableinfo";

            //Create a list to store the result
            string returnString = "";

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                int n = dataReader.FieldCount;
                while (dataReader.Read())
                {
                    for (int i = 0; i < n ; i++)
                    {
                        returnString = returnString + dataReader[i] + ">";
                    }

              
                    returnString = returnString + "#";
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
             CloseConnection();

                //return list to be displayed
                return returnString;
            }
            else
            {
                return returnString;
            }
        }

        ////Count statement
        public static int Count(string query)
        {
          //  string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
               CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public static void Backup()
        {
        }

        //Restore
        public static void Restore()
        {
        }


    }

