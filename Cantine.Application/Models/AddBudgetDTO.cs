using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Application.Models
{
    public class AddBudgetDTO
    {
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
    }
}
