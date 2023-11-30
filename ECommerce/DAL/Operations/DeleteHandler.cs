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
using System.Threading.Tasks.Dataflow;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ECommerce.DAL.Operations
{
    public class DeleteHandler : Main
    {
        public static void DeleteFromSqlDb(List<Product> ListOfProducts)
        {
            SqlVariables.SetSqlVariables(out var adapter, out var sql, out var cnn);

            cnn.Open();
            command = new SqlCommand(sql,cnn);
            //command.CommandText = "Select NameOfProduct from dbo.Product";
            command.CommandText = "Select NameOfProduct from dbo.Product";

            var existingProductNames = new List<string>();
            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    existingProductNames.Add(dataReader.GetString(0));
                }
            }

            foreach (var productName in existingProductNames)
            {
                if (!ListOfProducts.Any(p => p.NameOfProduct == productName))
                {
                    command.CommandText = $"Delete from dbo.Product where NameOfProduct= @productName";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@productName", productName);

                    command.ExecuteNonQuery();
                }
            }

            cnn.Close();
        }
    }
}
