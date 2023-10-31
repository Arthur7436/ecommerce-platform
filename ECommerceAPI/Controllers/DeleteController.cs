using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ECommerceAPI.Controllers
{
    public class DeleteController :ControllerBase
    {
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
