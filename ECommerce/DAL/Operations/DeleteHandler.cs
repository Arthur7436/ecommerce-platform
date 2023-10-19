using ECommerce.Business_Logic.Operations;
using ECommerce.Database;
using ECommerce.GlobalVariables;
using ECommerce.Models;
using System;
using System.Collections.Generic;
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
            SqlVariables.SetSqlVariables(out adapter, out sql, out cnn);
            cnn.Open();

            //read all the product names in sql db and store it
            //assign connection
            SqlDataReader dataReader;
            string sql1, Output = "";

            //cnn = new SqlConnection(connectionString);
            command = new SqlCommand(sql, cnn);
            command.CommandText = "Select NameOfProduct from dbo.Product";
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + "\n";
            }

            Console.WriteLine(Output);



            //go through the entire list of products
            //for (int i = 0; i < ListOfProducts.Count; i++)
            //{

            //    if (ListOfProducts[i] == )
            //    {

            //    }
            //}

            ////compare it with sql db

            ////the one that stands out is to be removed

            ////remove product also from sql db
            //sql = $"Delete dbo.Product where Identify={i + 1}";

            //command = new SqlCommand(sql, cnn);
            ////cnn.Open();
            //adapter.DeleteCommand = new SqlCommand(sql, cnn);
            //adapter.DeleteCommand.ExecuteNonQuery();

            //CloseSqlConnection.CloseSql();

        }
    }
}
