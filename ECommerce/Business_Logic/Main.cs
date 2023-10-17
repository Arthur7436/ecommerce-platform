using ECommerce.Database;
using ECommerce.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;


namespace ECommerce.Repository
{
    public class Main
    {
        public static SqlCommand? command;
        public static SqlDataAdapter? adapter;
        public static string? sql;
        public static SqlConnection? cnn;

    }
}
