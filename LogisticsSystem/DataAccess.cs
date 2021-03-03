using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LogisticsSystem.Models;

namespace LogisticsSystem
{
    public class DataAccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        public List<Item> GetItemList()
        {
            List<Item> result = new List<Item>();

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT " +
                    "ItemID, " +
                    "ItemName, " +
                    "Weight, " +
                    "Cost, " +
                    "Priority, " +
                    "Type " +
                    "FROM Item";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Item item = new Item();
                        item.ItemID = Convert.ToInt32(dataReader["ItemID"]);
                        item.ItemName = dataReader["ItemName"].ToString();
                        item.Weight = Convert.ToDecimal(dataReader["Weight"]);
                        item.Cost = Convert.ToDecimal(dataReader["Cost"]);
                        item.Priority = Convert.ToInt32(dataReader["Priority"]);
                        item.Type = dataReader["Type"].ToString();
                        result.Add(item);
                    }
                }
                connection.Close();
            }
            return result;
        }

        public bool AddItem(Item item)
        {
            int result;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "proc_AddItem";
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@itemName", item.ItemName);
                command.Parameters.AddWithValue("@weight", item.Weight);
                command.Parameters.AddWithValue("@cost", item.Cost);
                command.Parameters.AddWithValue("@priority", item.Priority);
                command.Parameters.AddWithValue("@type", item.Type);                
                result = command.ExecuteNonQuery();
                connection.Close();
            }

            return result < 0 ? true : false;
        }

        public bool EditItem(Item item)
        {
            int result;            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "proc_EditItem";
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@itemID", item.ItemID);
                command.Parameters.AddWithValue("@itemName", item.ItemName);
                command.Parameters.AddWithValue("@weight", item.Weight);
                command.Parameters.AddWithValue("@cost", item.Cost);
                command.Parameters.AddWithValue("@priority", item.Priority);
                command.Parameters.AddWithValue("@type", item.Type);
                result = command.ExecuteNonQuery();
                connection.Close();
            }

            return result < 0 ? true : false;
        }

        public bool DeleteItem(string itemID)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "proc_DeleteItem";
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@itemID", itemID);                
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return result < 0 ? true : false;
        }
    }
}