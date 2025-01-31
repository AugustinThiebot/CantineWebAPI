using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.Application.Errors
{
    public sealed class ClientNotFoundException(Guid clientId): Exception($"Unknown client : {clientId}")
    {
    }
}
