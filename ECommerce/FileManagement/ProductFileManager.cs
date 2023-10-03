using ECommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class ProductFileManager
    {
        public static void SerializeToJsonFile(List<Product> ListOfProducts)
        {
            string json = $"{JsonConvert.SerializeObject(ListOfProducts, Formatting.Indented)}";
            File.WriteAllText(@"C:\FileStorage\Test.json", json); //add ListOfProducts <List> into JSON file
        }

        public static List<Product> DeserializeJsonFileToList()
        {
            List<Product> ListOfProducts;
            //turn the Json file into ListOfProducts so that memory is stored
            string storedJsonMemory = File.ReadAllText(@"C:\FileStorage\Test.json");
            ListOfProducts = JsonConvert.DeserializeObject<List<Product>>(storedJsonMemory)!;
            return ListOfProducts;
        }
    }
}
