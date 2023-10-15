using ECommerce.FileManagement;
using ECommerce.Models;
using ECommerce.Repository;


namespace ECommercePlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Product> ListOfProducts = new List<Product>(); //create a list to store all products inside
            ProductDataBaseHandler.ConnectToSqlDb(); //connect program to database
            CheckForFileAndDirectory.CheckForDirectory();
            CheckForFileAndDirectory.CheckForFile();

  

            do
            {
                ProductDataBaseHandler.InstantiateJsonFileFromSqlDb(ListOfProducts); //json file is to reflect sql db at all times

                ListOfProducts = ProductFileManager.DeserializeJsonFileToList(); //allows product stored in file as memory upon start up

               // ProductDataBaseHandler.MakeIdentifyColumnNumberingUpToDate(); //Makes SQL db column for "Identify" in chronological numerical sequence 

                ProductRepository.DisplayMenu();//Display the menu to user

                string? input = Console.ReadLine(); //store the users input into variable to determine program flow 

                switch (input)
                {
                    case "q": //quit the program
                
                        Environment.Exit(0);
                        return; //close the program

                    case "r": //reset program memory
                
                    ProductRepository.ClearProductList(ListOfProducts); //reset the list and json file
                        break;
                case "1": //view all products available
                    ProductRepository.ViewProduct(ListOfProducts); //views what is in list & JSON file

                    ProductDataBaseHandler.ViewSqlDb(); //views what is in db

                    Thread.Sleep(500);
                        break;
                case "2": //add the product requested by user via the console application
                
                    ProductRepository.AddProductToListAndSqlDb(ListOfProducts!); //Add product to JSON file and SQL db
                    ProductFileManager.SerializeToJsonFile(ListOfProducts); //Serialize the updated list to the JSON file
                        break;
                case "3": //remove the product requested by user
                    ProductRepository.RemoveProduct(ListOfProducts!);//removes the requested product
                    ProductFileManager.SerializeToJsonFile(ListOfProducts);//Serialize the updated list to the JSON file
                        break;  
                case "4": //update the product requested by user
                    ProductRepository.UpdateProduct(ListOfProducts!);//Updates the products name or description in both JSON file and SQL db
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
            }
            } while (true);
        }
    }
}
