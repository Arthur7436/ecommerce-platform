using ECommerce.Database;
using ECommerce.GlobalVariables;
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
        public void DeleteFromSqlDb(userRemovalInput)
        {
            SqlVariables.SetSqlVariables(out adapter, out sql, out cnn);
            cnn.Open();

            //remove product also from sql db
            sql = $"Delete dbo.Product where Identify={i + 1}";

            command = new SqlCommand(sql, cnn);
            //cnn.Open();
            adapter.DeleteCommand = new SqlCommand(sql, cnn);
            adapter.DeleteCommand.ExecuteNonQuery();

            CloseSqlConnection.CloseSql();

        }
}
