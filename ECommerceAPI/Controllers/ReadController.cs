using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using ECommerce.FileManagement;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/Ecommerce")]
    public class ReadController : ControllerBase
    {
        private readonly ILogger<ReadController> _logger;

        public ReadController(ILogger<ReadController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProduct")]
        public IActionResult Get()
        {
            //string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!; //used SETX command to store SQL_PASSWORD into local machine so that credentials are not hard-coded

            //Attempt to connect console application to server database

            //variable declaration
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source={DataBaseHandler.dataSourceName};Initial Catalog={DataBaseHandler.dataBase};User ID={DataBaseHandler.username};Password={DataBaseHandler.pwd}";

            //assign connection
            cnn = new SqlConnection(connectionString);

            //create sql commands to be able to read from db
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";
            sql = "Select Identify,Id,NameOfProduct,Description from dbo.Product";

            //Open connection
            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            //create a list to store the output
            List<Product> products = new List<Product>();

            while (dataReader.Read())
            {
                Product product = new Product();
                product.Id = (string)dataReader["Id"];
                product.NameOfProduct = (string)dataReader["NameOfProduct"];
                product.Description = (string)dataReader["Description"];
                products.Add(product);
            }

            dataReader.Close();
            command.Dispose();
            cnn.Close();

            //serialize the list into json format
            //string json = JsonConvert.SerializeObject(products, Formatting.Indented);
            string json = JsonConvert.SerializeObject(products, Formatting.Indented);

            return Ok($"Reading database successful! \n {json}");
            

        }
    }
}