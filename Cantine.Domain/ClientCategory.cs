using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Domain
{
    public class ClientCategory
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string DiscountType { get; set; } = "None";
        [Required]
        public decimal DiscountValue { get; set; } = 0;
    }
}
