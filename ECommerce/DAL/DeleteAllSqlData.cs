using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL
{
    public class DeleteAllSqlData
    {
        public static void DeleteAllFromSqlDb()
        {
            //variables
            string usernameSecret = File.ReadAllText(@"C:\ProjectSecrets\username.txt");
            string username = usernameSecret;
            string dataSourceName = File.ReadAllText(@"C:\ProjectSecrets\dataSourceName.txt");
            string dataSource = dataSourceName;
            string dataBase = "dbProduct";
            string path = @"C:\ProjectSecrets\EcommerceSecrets.txt";
            string pwd = File.ReadAllText(path);

            //Delete all in sql db
            SqlConnection cnn;
            string connectionString;
            connectionString = $"Data Source={dataSource};Initial Catalog={dataBase};User ID={username};Password={pwd}";

            cnn = new SqlConnection(connectionString);

            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "";

            sql = "Delete FROM dbo.Product";
            cnn.Open();
            command = new SqlCommand(sql, cnn);

            adapter.DeleteCommand = new SqlCommand(sql, cnn);
            adapter.DeleteCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();
        }
    }
}
