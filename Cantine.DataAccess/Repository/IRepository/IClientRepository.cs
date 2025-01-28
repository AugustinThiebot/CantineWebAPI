
using Cantine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.DataAccess.Repository.IRepository
{
    public interface IClientRepository
    {
        Task<Client> GetByIdAsync(Guid id);
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteByIdAsync(Guid id);
    }
}
