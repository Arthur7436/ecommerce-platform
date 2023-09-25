using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/Ecommerce")]
    public class ECommerceController : ControllerBase
    {
        private readonly ILogger<ECommerceController> _logger;

        public ECommerceController(ILogger<ECommerceController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProduct")]
        public IActionResult Get()
        {
            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!; //used SETX command to store SQL_PASSWORD into local machine so that credentials are not hard-coded

            //Attempt to connect console application to server database

            //variable declaration
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";

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

            return Ok(json);

        }


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

            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";
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

            //store the var in sql str below
            sql = $"Insert into dbo.Product (Identify,Id,NameOfProduct,Description) values('" + $"{numOfProducts + 1}" + "', '" + $"{product.Id}" + "', '" + $"{product.NameOfProduct}" + "' , '" + $"{product.Description}" + "')";
            cnn.Open();
            command = new SqlCommand(sql, cnn);
            adapter.InsertCommand = new SqlCommand(sql, cnn);

            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();

            return Ok();
        }


        [HttpPut]
        [Route("/put")]
        public IActionResult Put([FromBody] Product product)
        {
            //open connection
            //set sql variables
            SqlDataReader dataReader;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "select * from dbo.Product";

            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";
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
                identify.NameOfProduct = (string)dataReader["NameOfProduct"];
                identify.Description = (string)dataReader["Description"];
                products.Add(identify);
            }

            command.Dispose();
            cnn.Close();

            //Update the product details based on Id
            var update = products.FirstOrDefault(x => x.Id == product.Id);

            int index = products.IndexOf(update); //find the index of the newProductName in the list

            //store the new json inputs into variables which would later be uysed to update the current product
            string newName = product.NameOfProduct;
            string newDescription = product.Description;

            if (update != null)
            {
                sql = $"delete dbo.Product set NameOfProduct = '{newName}', Description = '{newDescription}' where Identify={index + 1}";
                //sql = "Update dbo.Product set NameOfProduct='" + $" values {newName}" + $"{newDescription}" + $"' where Identify={index + 1}";

                cnn.Open();
                command = new SqlCommand(sql, cnn);
                adapter.UpdateCommand = new SqlCommand(sql, cnn);
                adapter.UpdateCommand.ExecuteNonQuery();

                cnn.Dispose();
                cnn.Close();
            }


            //update the product based on product name


            return Ok();
        }

        [HttpDelete]
        [Route("/delete")]
        public IActionResult Delete([FromBody] Product product)
        {

            //open connection
            //set sql variables
            SqlDataReader dataReader;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "select * from dbo.Product";

            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";
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
                identify.NameOfProduct = (string)dataReader["NameOfProduct"];
                identify.Description = (string)dataReader["Description"];
                products.Add(identify);
            }

            command.Dispose();
            cnn.Close();

            //delete the product based on id
            //Update the product details based on Id
            var delete = products.FirstOrDefault(x => x.Id == product.Id);

            int index = products.IndexOf(delete); //find the index of the newProductName in the list

            //store the new json inputs into variables which would later be uysed to update the current product
            string newName = product.NameOfProduct;
            string newDescription = product.Description;

            if (delete != null)
            {
                sql = $"DELETE dbo.Product where Identify={index + 1}";
                //sql = $"delete dbo.Product set NameOfProduct = '{newName}', Description = '{newDescription}' where Identify={index + 1}";
                //sql = "Update dbo.Product set NameOfProduct='" + $" values {newName}" + $"{newDescription}" + $"' where Identify={index + 1}";

                cnn.Open();
                command = new SqlCommand(sql, cnn);
                adapter.DeleteCommand = new SqlCommand(sql, cnn);
                adapter.DeleteCommand.ExecuteNonQuery();

                cnn.Dispose();
                cnn.Close();
            }

            return Ok("deleted successfully");

        }
    }
}