using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Domain
{
    public class ProductDetail
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
