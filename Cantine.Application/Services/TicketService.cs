using Cantine.DataAccess.Repository.IRepository;
using Cantine.Application.Services.IServices;
using Cantine.Application.Strategies;
using Cantine.Application.Models;
using Cantine.Domain;
using Cantine.Application.Errors;

namespace Cantine.Application.Services
{
    public class TicketService: ITicketService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        public TicketService(IClientRepository clientRepository, IProductRepository productRepository)
        {
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public async Task<TicketDTO> GenerateTicketAsync(TicketRequestDTO ticketRequestDTO)
        {
            try
            {
                Guid clientId = ticketRequestDTO.ClientId;
                List< string > products = ticketRequestDTO.Products;
                var client = await _clientRepository.GetByIdAsync(clientId);
                if (client == null) {
                    throw new ClientNotFoundException(clientId);
                }
                var allProductsDictionary = await _productRepository.GetAllAsync();
                var menuDictionary = new Dictionary<string, int>
                {
                    {"Pain", products.Count(p => p == "Pain")},
                    {"Entrée", products.Count(p => p == "Entrée") },
                    {"Plat", products.Count(p => p == "Plat") },
                    {"Dessert", products.Count(p => p == "Dessert") }
                };
                int nbMenus = menuDictionary.Values.Min();
                decimal menuReduction = (allProductsDictionary["Pain"] + allProductsDictionary["Entrée"] + allProductsDictionary["Plat"] + allProductsDictionary["Dessert"]) - 10;
                decimal ticketCost = products.Sum(p => allProductsDictionary[p]) - menuReduction* nbMenus;
                
                ClientCategory clientCategory = client.Category;
                decimal totalToPay = DiscountStrategyFactory.GetStrategy(clientCategory.DiscountType).ApplyDiscount(ticketCost, clientCategory.DiscountValue);


                if (totalToPay > client.Budget && client.Category.Name != "VIP" && client.Category.Name != "Interne")
                {
                    throw new BudgetTooLowException(totalToPay);
                }
                client.Budget -= totalToPay;
                await _clientRepository.UpdateAsync(client);
                

                List <ProductDetail> productDetails = products.Select(p => new ProductDetail { ProductName = p, Price = allProductsDictionary[p] }).ToList();

                var ticket = new TicketDTO
                {
                    ClientID = clientId,
                    Products = productDetails,
                    TotalToPay = totalToPay,
                };
                return ticket;
            }
            catch (Exception)
            {
                throw;
            }


        }
    }
}
