using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregationForNASTI
{
    public static class DataBaseController
    {
      
      

        private static string GetConnectionString(string dataBase)
        {
            return (new MySqlConnectionStringBuilder
            {
                //Server = DbData.Server,
                //UserID = DbData.UserID,
                //Password = DbData.Password,
                //Database = dataBase
                Server = "5.132.159.203",
                UserID = "alex",
                Password = "Minsk2017!",
                Database = dataBase
            })
            .ConnectionString;
        }

        public static List<List<string>> SelectFromTable(string dataToSelect, string table, string condition, string dataBase)
        {
            List<List<string>> loadedData = new List<List<string>>();
            List<string> row = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(dataBase)))
            {
                using (MySqlCommand query = connection.CreateCommand())
                {
                    connection.Open();

                    if (condition.Length > 0)
                    {

                        using (MySqlCommand command = new MySqlCommand(
                "SELECT " + dataToSelect + " FROM " + table + " Where " + condition, connection))
                        {
                            MySqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                for (int i = 0; i < dataToSelect.Split(',').Count(); i++)
                                {
                                    row.Add(reader[i].ToString());
                                }
                                loadedData.Add(row);
                                row = new List<string>();
                            }
                        }
                    }
                    else
                    {
                        using (MySqlCommand command = new MySqlCommand(
             "SELECT " + dataToSelect + " FROM " + table, connection))
                        {
                            MySqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                for (int i = 0; i < dataToSelect.Split(',').Count(); i++)
                                {
                                    row.Add(reader[i].ToString());
                                }
                                loadedData.Add(row);
                                row = new List<string>();
                            }
                        }
                    }

                }

            }

            return loadedData;

        }

        public static List<List<string>> GetDataFromDatabase(string queryString, string dataBase, int countInRow)
        {
            List<List<string>> loadedData = new List<List<string>>();
            List<string> row = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(dataBase)))
            {
                using (MySqlCommand query = connection.CreateCommand())
                {
                    connection.Open();


                    using (MySqlCommand command = new MySqlCommand(
           queryString, connection))
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            for (int i = 0; i < countInRow; i++)
                            {
                                row.Add(reader[i].ToString());
                            }
                            loadedData.Add(row);
                            row = new List<string>();
                        }
                    }
                }
            }
            return loadedData;

        }

        public static void PostQueryToDataBase(string queryString, string dataBase)
        {

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(dataBase)))
            {
                using (MySqlCommand query = connection.CreateCommand())
                {
                    connection.Open();


                    using (MySqlCommand command = new MySqlCommand(queryString, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        public static void UpdateInTable(string dataToUpdate, string newData, string table, string condition, string dataBase)
        {
            List<string> loadedData = new List<string>();

            using (MySqlConnection connection = new MySqlConnection(GetConnectionString(dataBase)))
            {
                using (MySqlCommand query = connection.CreateCommand())
                {
                    connection.Open();

                    if (condition.Length > 0)
                    {

                        using (MySqlCommand command = new MySqlCommand(
                "UPDATE " + table + " SET " + dataToUpdate + "=" + newData + " Where " + condition, connection))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }


        }

    }
}