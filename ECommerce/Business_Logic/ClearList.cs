using ECommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business_Logic
{
    public class ClearList
    {
        public static void ClearAllList(List<Product> ListOfProducts)
        {
            //remove everything in the list
            ListOfProducts.Clear();
        }
    }
}
