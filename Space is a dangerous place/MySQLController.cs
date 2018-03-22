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
        string query;

        string row;
        public List<string> tableContent = new List<string>();
        public Dictionary<string, int> highscores = new Dictionary<string, int>();


        public MySQLController()
        {

            connection = new MySqlConnection(myConnectionString);
            
        }

        public void ReadTable(string tableName, string order)
        {

            tableContent.Clear();

            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + tableName + " ORDER BY score " + order;
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

            query = "INSERT INTO highscores (name, score) Values('" + name + "', " + score + ")";
            command = new MySqlCommand(query);
            command.Connection = connection;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }

        public void deleteHighscore(string name)
        {
            query = "DELETE FROM highscores WHERE name = '" + name + "';";
            command = new MySqlCommand(query);
            command.Connection = connection;

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public bool isScoreHigher(string name)
        {
            int score = 0;

            query = "SELECT `score` FROM highscores WHERE name = '" + name + "';";
            command = new MySqlCommand(query);
            command.Connection = connection;
            
            connection.Open();
            reader = command.ExecuteReader();

            reader.Read();
            if(reader.HasRows)
                score = Convert.ToInt16(reader.GetValue(0));
                
            connection.Close();

            return Properties.Settings.Default.Highscore > score;
        }

        public void formatHighscores()
        {
            highscores.Clear();
            char divider = ' ';
            string[] helperArray;
            List<string> dividedStrings = new List<string>();
            List<string> stringHelperList = new List<string>();
            List<int> intHelperList = new List<int>();

            ReadTable("highscores", "DESC");

            for (int i = 0; i < tableContent.Count; i++)
            {
                helperArray = tableContent[i].Split(divider);
                for (int ii = 0; ii < helperArray.Length - 1; ii++)
                {
                    helperArray[ii] = helperArray[ii].Remove(helperArray[ii].Length - 1);
                    dividedStrings.Add(helperArray[ii]);
                }
            }

            for (int i = 0; i < dividedStrings.Count; i++)
            {
                if (i % 2 == 0)
                    stringHelperList.Add(dividedStrings[i]);
                else
                    intHelperList.Add(Convert.ToInt32(dividedStrings[i]));
            }

            for (int i = 0; i < stringHelperList.Count; i++)
                highscores.Add(stringHelperList[i], intHelperList[i]);
        }
    }
}
