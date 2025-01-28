using Cantine.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cantine.DataAccess.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, decimal>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            var productDictionary = products.ToDictionary(p => p.ProductName, p => p.Price);
            return productDictionary;
        }
    }
}
