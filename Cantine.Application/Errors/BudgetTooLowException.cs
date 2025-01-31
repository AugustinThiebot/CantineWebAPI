using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Application.Errors
{
    public sealed class BudgetTooLowException(decimal totalToPay): Exception($"Current client's budget is too low to pay {totalToPay}")
    {
    }
}
