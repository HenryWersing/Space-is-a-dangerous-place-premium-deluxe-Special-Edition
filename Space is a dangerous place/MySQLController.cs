using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Space_is_a_dangerous_place
{
    class MySQLController
    {

        string myConnectionString = "SERVER=10.33.156.236;" + "DATABASE=siadp_database;" + "UID=user;" + "PASSWORD=qwer4321;";

        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;
        string insertQuery;

        string row;
        public List<string> tableContent = new List<string>();


        public MySQLController()
        {

            connection = new MySqlConnection(myConnectionString);
            
        }

        public void ReadTable(string tableName)
        {

            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + tableName;
            connection.Open();
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                row = "";

                for (int i = 0; i < reader.FieldCount; i++)
                    row += reader.GetValue(i).ToString() + ", ";

                tableContent.Add(row);
            }

            connection.Close();

        }
        
        public void insertHighscore(string name, int score)
        {

            insertQuery = "INSERT INTO highscores (name, score) Values('" + name + "', " + score + ")";
            command = new MySqlCommand(insertQuery);
            command.Connection = connection;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
}
