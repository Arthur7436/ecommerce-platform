using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set;  }
        public List<Product> Products { get; set;  } = new List<Product>();
    }
}
