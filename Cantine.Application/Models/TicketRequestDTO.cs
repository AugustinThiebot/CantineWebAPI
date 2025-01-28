using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Application.Models
{
    public class TicketRequestDTO
    {
        public Guid ClientId { get; set; }
        public List<string> Products { get; set; }
    }
}
