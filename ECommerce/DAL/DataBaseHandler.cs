using ECommerce.FileManagement;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class DataBaseHandler
    {
        public static SqlCommand? command;
        public static SqlDataAdapter? adapter;
        public static string? sql;
        public static SqlConnection? cnn;
        public static string usernameSecret = File.ReadAllText(@"C:\ProjectSecrets\username.txt");
        public static string username = usernameSecret;
        public static string dataSourceName = File.ReadAllText(@"C:\ProjectSecrets\dataSourceName.txt");
        public static string dataSource = dataSourceName;
        public static string dataBase = "dbProduct";
        public static string path = @"C:\ProjectSecrets\EcommerceSecrets.txt";
        public static string pwd = File.ReadAllText(path);

    }
}
