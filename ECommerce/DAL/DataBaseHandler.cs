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
        public string usernameSecret;
        public DataBaseHandler()
        { //Constructor is put in place so that username can initialise with another instance field. Without a constructor, you cannot initialise an instance field with another instance field within a class.
            usernameSecret = File.ReadAllText(@"C:\ProjectSecrets\username.txt");
            username = usernameSecret;
        }
        public string username; //store the file fath of username.txt in a variable
        public static string dataSourceName = File.ReadAllText(@"C:\ProjectSecrets\dataSourceName.txt");
        public static string dataSource = dataSourceName; //store the file fath of dataSourceName.txt in a variable
        public static string dataBase = "dbProduct";
        public static string path = @"C:\ProjectSecrets\EcommerceSecrets.txt";
        public static string pwd = File.ReadAllText(path);

    }
}
