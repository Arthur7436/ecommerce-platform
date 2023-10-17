using ECommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.FileManagement
{
    public class ProductFileManager
    {
        public static void SerializeToJsonFile(List<Product> ListOfProducts) //add ListOfProducts <List> into JSON file
        {
            string json = $"{JsonConvert.SerializeObject(ListOfProducts, Formatting.Indented)}";
            File.WriteAllText(@"C:\FileStorage\Test.json", json); 
        }

        public static List<Product> DeserializeJsonFileToList() //turn the Json file into ListOfProducts so that memory is stored
        {
            List<Product> ListOfProducts;
            string storedJsonMemory = File.ReadAllText(@"C:\FileStorage\Test.json");
            ListOfProducts = JsonConvert.DeserializeObject<List<Product>>(storedJsonMemory)!;
            return ListOfProducts;
        }
    }
}
