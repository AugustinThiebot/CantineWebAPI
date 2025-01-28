using Cantine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Application.Models
{
    public class TicketDTO
    {
        public Guid ClientID { get; set; }
        public List<ProductDetail> Products { get; set; } = new List<ProductDetail>();
        public decimal TotalToPay { get; set; } = 0;
    }
}
