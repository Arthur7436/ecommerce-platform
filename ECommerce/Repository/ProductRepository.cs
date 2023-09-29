using ECommerce.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;


namespace ECommerce.Repository
{
    public class ProductRepository
    {
        public static SqlCommand command;
        public static SqlDataAdapter adapter;
        public static string sql;
        public static SqlConnection cnn;

        public static void InstantiateJsonFileFromSqlDb(List<Product> ListOfProducts)
        {
            //Make sql db as SOT and store in the file at the beginning
            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!; //used SETX command to store SQL_PASSWORD into local machine so that credentials are not hard-coded
            Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("Storage of password in variable was successful...");
            Console.ResetColor();
            Thread.Sleep(500);

            //Attempt to connect console application to server database

            //variable declaration
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";

            //assign connection
            cnn = new SqlConnection(connectionString);

            //See if the connection works
            try //if connection to db is successful
            {
                cnn.Open();

            }
            catch (Exception ex) //if connection to db is unsuccessful
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot open connection... ");
                Console.ResetColor();
                Thread.Sleep(3000);
            }

            //create sql commands to be able to read from db
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";
            sql = "Select Identify,Id,NameOfProduct,Description from dbo.Product";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();


            //convert what is in the db and deserialize into json file
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

            ProductRepository.SerializeToJsonFile(products); //serializes the most up to date list into a json file
        }
        public static List<Product> DeserializeJsonFileToList()
        {
            List<Product> ListOfProducts;
            //turn the Json file into ListOfProducts so that memory is stored
            string storedJsonMemory = File.ReadAllText(@"C:\FileStorage\Test.json");
            ListOfProducts = JsonConvert.DeserializeObject<List<Product>>(storedJsonMemory)!;
            return ListOfProducts;
        }

        public static void SerializeToJsonFile(List<Product> ListOfProducts)
        {
            string json = $"{JsonConvert.SerializeObject(ListOfProducts, Formatting.Indented)}";
            File.WriteAllText(@"C:\FileStorage\Test.json", json); //add ListOfProducts <List> into JSON file
        }

        public static void ConnectToSqlDb()
        {
            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!; //used SETX command to store SQL_PASSWORD into local machine so that credentials are not hard-coded
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Storage of password in variable was successful...");
            Console.ResetColor();
            Thread.Sleep(500);


            //Attempt to connect console application to server database

            //variable declaration
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";

            //assign connection
            cnn = new SqlConnection(connectionString);

            //See if the connection works
            try //if connection to db is successful
            {
                cnn.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Connection to SQL database was successful... ");
                Console.ResetColor();
                Thread.Sleep(500);
                //cnn.Close(); Move this to TurnOffConnectionToDb method when user enters q
            }
            catch (Exception ex) //if connection to db is unsuccessful
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot open connection... ");
                Console.ResetColor();
                Thread.Sleep(3000);
                Environment.Exit(0); //exit program
            }
        }
        //public static void TurnOffConnectionToDb()
        //{
        //    string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!; //used SETX command to store SQL_PASSWORD into local machine so that credentials are not hard-coded

        //    string connectionString = null!;
        //    SqlConnection cnn;

        //    connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=athai;Password={pwd}";
        //    cnn = new SqlConnection(connectionString);

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Database connection closing...");
        //    Console.ResetColor();
        //    cnn.Close();
        //    Thread.Sleep(500);
        //}

        public static void MakeIdentifyColumnNumberingUpToDate()
        {
            SetSqlVariables(out adapter, out sql, out cnn);

            cnn.Open();

            //Make Identify to be sequential numbering
            sql = "DECLARE @id INT SET @id = 0 UPDATE dbo.Product SET @id = Identify = @id + 1";

            command = new SqlCommand(sql, cnn);
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();

            CloseSqlConnection();
        }

        private static void CloseSqlConnection()
        {
            command.Dispose();
            cnn.Close();
        }

        private static void SetSqlVariables(out SqlDataAdapter adapter, out string sql, out SqlConnection cnn)
        {
            //set sql variables
            SqlCommand command;
            adapter = new SqlDataAdapter();
            sql = "";
            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;
            string connectionString = null!;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";
            cnn = new SqlConnection(connectionString);
        }

        public static void UpdateProduct(List<Product> ListOfProducts)
        {
            SetSqlVariables(out adapter, out sql, out cnn);
            
            //set sql variables
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";

            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";
            cnn = new SqlConnection(connectionString);

            //Ask the user
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Which product did you want to update: ");
            Console.ResetColor();

            foreach (Product products in ListOfProducts)
            {
                Console.WriteLine(products);
            }

            string? UserInput = Console.ReadLine();
            //loop through list and if the name of product equals userInput then ask which they want to update: NameOfProduct or Description
            List<string> productDetails = new List<string>();
            productDetails.Add("Name of Product");
            productDetails.Add("Description of product");



            if (ListOfProducts.Any(x => x.NameOfProduct == UserInput)) //if user selects a product via NameOfProduct
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Which property did you want to update for '{UserInput}':");
                Console.ResetColor();

                for (int j = 0; j < (productDetails.Count); j++)
                {
                    Console.WriteLine($"{j + 1}: {productDetails[j]}"); //display properties of Product that can be updated
                    //if we have reached to the last option of the list then break

                }

                int? numInput = int.Parse(Console.ReadLine());
                if (numInput == 1) //<-------HANDLE SYSTEM IF USER INPUTS NOT A NUMBER -------->
                {
                    Console.WriteLine("Enter new product name: ");
                    string? newProductName = Console.ReadLine();

                    //update the Object's property NameOfProduct
                    //go through whole list and find the object's index
                    for (int i = 0; i < ListOfProducts.Count; i++)
                    {
                        if (ListOfProducts[i].NameOfProduct == UserInput)
                        {
                            ListOfProducts[i].NameOfProduct = newProductName;
                            ProductRepository.SerializeToJsonFile(ListOfProducts); //serialize to json file so that it would not be overwritten at the start of Main Program
                            //update in sql db
                            cnn.Open();
                            sql = "Update dbo.Product set NameOfProduct='" + $"{newProductName}" + $"' where Identify={i + 1}"; //Update the column NameOfProduct at the row of that product

                            command = new SqlCommand(sql, cnn);

                            adapter.UpdateCommand = new SqlCommand(sql, cnn);
                            Console.WriteLine(adapter.UpdateCommand.ExecuteNonQuery());
                            adapter.UpdateCommand.ExecuteNonQuery();

                            command.Dispose();
                            cnn.Close();



                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Product name updated!");
                            Console.ResetColor();
                            Thread.Sleep(500);
                        }
                    }
                }
                else if (numInput == 2)
                {
                    Console.WriteLine("Enter new product description: ");
                    string? newProductDescription = Console.ReadLine();
                    //update the Object's property NameOfProduct
                    //go through whole list and find the object's index
                    for (int i = 0; i < ListOfProducts.Count; i++)
                    {
                        if (ListOfProducts[i].NameOfProduct == UserInput)
                        {
                            ListOfProducts[i].Description = newProductDescription;
                            ProductRepository.SerializeToJsonFile(ListOfProducts); //serialize to json file so that it would not be overwritten

                            //update in sql db
                            cnn.Open();
                            sql = "Update dbo.Product set Description='" + $"{newProductDescription}" + $"' where Identify={i + 1}"; //Update the column Description at the row of that product

                            command = new SqlCommand(sql, cnn);

                            adapter.UpdateCommand = new SqlCommand(sql, cnn);
                            adapter.UpdateCommand.ExecuteNonQuery();

                            command.Dispose();
                            cnn.Close();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Product description updated!");
                            Console.ResetColor();
                            Thread.Sleep(500);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Product doesn't exist");

            }
        }

        public static void ViewSqlDb()
        {
            //create sql commands to be able to read from db
            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;

            sql = "Select Identify,Id,NameOfProduct,Description from dbo.Product";

            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";

            //assign connection
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2) + " - " + dataReader.GetValue(3) + "\n";
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("SQL database:");
            Console.ResetColor();
            Console.WriteLine(Output);
            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        public static void ClearProductList(List<Product> ListOfProducts)
        {
            //remove everything in the list
            ListOfProducts.Clear();
            //push those changes and serialize as Json 
            string json = JsonConvert.SerializeObject(ListOfProducts);
            File.WriteAllText(@"C:\FileStorage\Test.json", json);
        }

        public static void ViewProduct(List<Product> ListOfProducts) //ListOfProducts <List> is already deserialized into a list from the file
        {
            if (ListOfProducts == null || ListOfProducts.Count == 0) //give error message if list is empty
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No products to view!");
                Console.ResetColor();

                Console.ReadLine();
            }
            else //list all the products
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Here is the list of all products:");
                Console.ResetColor();

                //display all objects within List
                foreach (Product products in ListOfProducts)
                {
                    Console.WriteLine(products.ToString());
                }

                Console.WriteLine();
            }
        }

        public static void RemoveProduct(List<Product> ListOfProducts)
        {
            //set sql variables
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";

            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            //collect info from user
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Which product would you like to delete?");
            Console.ResetColor();

            foreach (Product products in ListOfProducts!) //print out all the products available
            {
                Console.WriteLine(products.ToString());
            }

            ViewSqlDb();

            Console.Write("Input: ");
            string userRemovalInput = Console.ReadLine()!;

            //remove based on name of product
            if (!ListOfProducts.Any(x => x.NameOfProduct == userRemovalInput)) //if users inputs a product that already exists => give error
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The product doesn't exist!");
                Console.ResetColor();

                Console.ReadLine();
                return;
            }

            if (ListOfProducts.Any(x => x.NameOfProduct == userRemovalInput)) //else remove the product from the list
            {
                //loop through the whole list 
                for (int i = 0; i < ListOfProducts.Count; i++)
                {
                    //if the name of product is equal to the userRemovalInput, find the index of that object
                    if (ListOfProducts[i].NameOfProduct == userRemovalInput)
                    {
                        //remove from list
                        ListOfProducts.RemoveAt(i);

                        //remove product also from sql db
                        sql = $"Delete dbo.Product where Identify={i + 1}";

                        command = new SqlCommand(sql, cnn);
                        cnn.Open();
                        adapter.DeleteCommand = new SqlCommand(sql, cnn);
                        adapter.DeleteCommand.ExecuteNonQuery();

                        command.Dispose();
                        cnn.Close();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Product successfully removed!");
                        Console.ResetColor();

                        Console.ReadLine();
                    }
                }
            }
        }

        public static void AddProductToListAndSqlDb(List<Product> ListOfProducts)
        {
            //set sql variables
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";

            string pwd = Environment.GetEnvironmentVariable("SQL_PASSWORD", EnvironmentVariableTarget.Machine)!;
            string connectionString = null!;
            SqlConnection cnn;
            connectionString = $"Data Source=AUL0953;Initial Catalog=ProductDB;User ID=sa;Password={pwd}";
            cnn = new SqlConnection(connectionString);

            //Create instance and add details to the instance which will be added to the list

            Product product = new Product();

            string GenerateRandomID = Guid.NewGuid().ToString("N");

            Console.WriteLine($"Product Id: {GenerateRandomID}");
            product.Id = GenerateRandomID;

            Console.WriteLine("Name of product: ");
            string? productNameInput = Console.ReadLine();

            //if the name of product already exists, then notify user
            for (int i = 0; i < ListOfProducts.Count; i++)
            {
                if (ListOfProducts[i].NameOfProduct == productNameInput)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This product name already exists!");
                    Console.ResetColor();

                    Console.ReadLine();
                    return;
                }
            }

            product.NameOfProduct = productNameInput;

            Console.WriteLine("Description of product: ");
            string? DescriptionOfProductInput = Console.ReadLine();
            product.Description = DescriptionOfProductInput!;

            ListOfProducts!.Add(product);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product added!");
            Console.ResetColor();

            //Loop through the list and find the row number + Id + NameOfProduct + Description in order to be sent to sql db
            for (int i = 0; i < ListOfProducts.Count; i++)
            {
                sql = $"Insert into dbo.Product (Identify,Id,NameOfProduct,Description) values({i + 1},'" + $"{product.Id}" + "', '" + $"{product.NameOfProduct}" + "' , '" + $"{product.Description}" + "')";
            }

            //push the product into sql db
            command = new SqlCommand(sql, cnn);
            adapter.InsertCommand = new SqlCommand(sql, cnn);

            cnn.Open();
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            cnn.Close();

            Console.ReadLine();
        }

        public static void DisplayMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Welcome to the E-commerce platform!");
            Console.ResetColor();
            Console.WriteLine("Please select one of the following: ");

            List<string> displayMenu = new List<string>()
                {
                    "1. View all products",
                    "2. Add a product",
                    "3. Remove a product",
                    "4. Update a product",
                    "Enter 'r' to reset the list to null",
                    "Enter 'q' to exit the program"
                };


            for (int i = 0; i < displayMenu.Count; i++)
            {
                Console.WriteLine(displayMenu[i]); //display menu
            }
        }
        public static void CheckForDirectory(){
            //create folder if doesn't exist
            string path = @"c:\FileStorage";

            try
            {
                if (Directory.Exists(path)) //if directory exists then return
                {
                    return;
                }

                //create directory
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine($"Directory was successfully created at {Directory.GetCreationTime(path)}");

                //create file in directory


            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.ToString}"); //give error message 

            }
            finally { }
        }

        public static void CheckForFile()
        {
            //create a file in directory if it doesn't exist
            string filePath = @"c:\FileStorage\Test.json";

            try
            {
                if (Directory.Exists(filePath)) //id directory exists then return
                {
                    return;
                }

                using (FileStream fs = File.Create(filePath)) ; //create the file
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.ToString}");
            }
            finally { }
        }
    }
}
