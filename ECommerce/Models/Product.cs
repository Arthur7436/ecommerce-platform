using Ecommerce.Interfaces.IProductOperations;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class Product : IProduct
    {
        [ScaffoldColumn(false)] //allows the response body message to be returned in the controller (badrequest)
        public string? Id { get; set; }
        //[Required, StringLength(50), Display(Name = "NameOfProduct")]
        [ScaffoldColumn(false)]
        public string? NameOfProduct { get; set; }
        //[Required, StringLength(50), Display(Name = "Description")]
        [ScaffoldColumn(false)]
        public string? Description { get; set; }
      
        public override string ToString()
        {
            return $"Product Id: {Id} \n + Name of Product: {NameOfProduct} \n + Description of Product: {Description}";
        }
    }
}
