using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ProductDTO
    {
        public int productId { get; set; }
        public string name { get; set; }
        public int typeid { get; set; }
        public string typeName { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public string picURL { get; set; }
        public int updatedBy { get; set; }
        public int IsActive { get; set; }
        public DateTime updatedOn { get; set; }
    }
}
