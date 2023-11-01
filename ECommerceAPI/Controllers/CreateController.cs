using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ECommerceAPI.Controllers
{
    public class CreateController :ControllerBase
    {
        //CREATE VALIDATION LOGIC THAT DOESN'T ACCEPT INVALID RESPONSES TO DB
        [HttpPost]
        [Route("/post")]
        public IActionResult Post([FromBody] Product product) //deserialize incoming request and map it against Product object 
        {
            //validate logic
            if (product.NameOfProduct == null || product.NameOfProduct == "")
            {
                return BadRequest("Product name is empty!");
            }

            if (product.Id == null || product.Id == "")
            {
                return BadRequest("Product Id is empty!");
            }

            if (product.Description == null || product.Description == "")
            {
                return BadRequest("Product description is empty!");
            }

            //set sql variables
            SqlDataReader dataReader;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "select * from dbo.Product";

            //string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;
            string connectionString = null!;
            SqlConnection cnn;
            //connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";
            connectionString = $"Data Source={DataBaseHandler.dataSourceName};Initial Catalog={DataBaseHandler.dataBase};User ID={DataBaseHandler.username};Password={DataBaseHandler.pwd}";
            cnn = new SqlConnection(connectionString);

            //go through the sql db, specifically the Identify column
            //Open connection
            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            //create a list to store the output
            List<Product> products = new List<Product>();

            while (dataReader.Read())
            {
                Product identify = new Product();
                identify.Id = (string)dataReader["Id"];
                products.Add(identify);
            }

            command.Dispose();
            cnn.Close();

            //find the exact amount of products in the list
            int numOfProducts = products.Count();

            //if duplicate then return error
            if (products.Any(x => x.Id == product.Id))
            {
                return BadRequest("This is a duplicate");
            }
            else
            {
                //else add to db

                //store the var in sql str below
                sql = $"Insert into dbo.Product (Identify,Id,NameOfProduct,Description) values('" + $"{numOfProducts + 1}" + "', '" + $"{product.Id}" + "', '" + $"{product.NameOfProduct}" + "' , '" + $"{product.Description}" + "')";
                cnn.Open();
                command = new SqlCommand(sql, cnn);
                adapter.InsertCommand = new SqlCommand(sql, cnn);

                adapter.InsertCommand.ExecuteNonQuery();

                command.Dispose();
                cnn.Close();

                return Ok($"Created successfully");
            }
        }
    }
}
