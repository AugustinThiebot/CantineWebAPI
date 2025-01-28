using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.DataAccess.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<Dictionary<string, decimal>> GetAllAsync();

    }
}
