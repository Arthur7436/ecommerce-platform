﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    //basic order class where products can be associated to
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set;  }
        public List<Product> Products { get; set;  } = new List<Product>(); //initialises Products list to make sure it is not null
        public override string ToString() //get output of class objects in string format
        {
            return $"Order ID: {OrderId} \n + Order date: {OrderDate} \n + List of Order: {Products.ToString}";
        }
    }
}
