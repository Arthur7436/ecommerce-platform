using ECommerce.Business_Logic.Operations;
using ECommerce.Database;
using ECommerce.GlobalVariables;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.DAL.Operations
{
    public class DeleteHandler : Main
    {
        public static void DeleteFromSqlDb(List<Product> ListOfProducts)
        {
            List<string> listOfOutputs = new List<string>(); //list to store all the existing values in sql column "NameOfProduct"
            SqlVariables.SetSqlVariables(out adapter, out sql, out cnn);
            cnn.Open();

            //read all the product names in sql db and store it in list "listOfOutputs"
            //assign connection
            SqlDataReader dataReader;
            string sql1, Output = "";

            //cnn = new SqlConnection(connectionString);
            command = new SqlCommand(sql, cnn);
            command.CommandText = "Select NameOfProduct from dbo.Product";
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    listOfOutputs.Add(dataReader[i].ToString()!);
                    //string dbName = dataReader[i].ToString()!;


                    foreach (string dbName in listOfOutputs)
                    {
                        if (!ListOfProducts.Any(x => x.NameOfProduct == dbName))
                        {
                            dataReader.Close();
                            sql = $"Delete from dbo.Product where NameOfProduct='{dbName}'";
                            adapter.DeleteCommand = new SqlCommand(sql, cnn);
                            adapter.DeleteCommand.ExecuteNonQuery();

                            CloseSqlConnection.CloseSql();
                            return;

                        }
                    }
                }
            }
        }
    }
}
