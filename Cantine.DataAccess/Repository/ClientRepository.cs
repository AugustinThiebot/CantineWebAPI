using Cantine.DataAccess.Repository.IRepository;
using Cantine.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.DataAccess.Repository
{
    public class ClientRepository: IClientRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            return await _context.Clients.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var client = await this.GetByIdAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

    }
}
