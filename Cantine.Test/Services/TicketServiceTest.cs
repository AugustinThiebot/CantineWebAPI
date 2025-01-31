using Cantine.DataAccess.Repository.IRepository;
using Moq;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cantine.Application.Services;
using Cantine.Domain;
using Cantine.Application.Models;
using Cantine.Application.Errors;

namespace Cantine.Test.Services
{
    public class TicketServiceTest
    {
        private readonly Mock<IClientRepository> _mockClientRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        public TicketServiceTest()
        {
            _mockClientRepository = new Mock<IClientRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task GenerateTicket_UnknownClient_ThrowsException()
        {
            var service = new TicketService(_mockClientRepository.Object, _mockProductRepository.Object);
            _mockClientRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Client)null);
            TicketRequestDTO ticketRequestDTO = new TicketRequestDTO
            {
                ClientId = Guid.NewGuid(),
                Products = new List<string> { "Pain" }
            };
            var exception = await Assert.ThrowsAsync<ClientNotFoundException>(() => service.GenerateTicketAsync(ticketRequestDTO));
            Assert.StartsWith("Unknown client : ", exception.Message);
        }

        [Theory]
        [InlineData("Interne", "6.3")]
        [InlineData("Prestataire", "7.8")]
        [InlineData("VIP", "0")]
        [InlineData("Stagiaire", "3.8")]
        [InlineData("Visiteur", "13.8")]
        public async Task GenerateTicket_CalculateTotalToPayWithDiscount_NoMenu(String clientType, String expectedTotalToPayStr)
        {
            var mockClient = clientType switch {
                "Interne" => MockData.GetMockInterneClient(),
                "Prestataire" => MockData.GetMockPrestataireClient(),
                "VIP" => MockData.GetMockVIPClient(),
                "Stagiaire" => MockData.GetMockStagiaireClient(),
                "Visiteur" => MockData.GetMockVisiteurClient(),
                _ => throw new Exception("Unknown client type")
            };
            decimal expectedTotalToPay = Convert.ToDecimal(expectedTotalToPayStr, CultureInfo.InvariantCulture);

            var service = new TicketService(_mockClientRepository.Object, _mockProductRepository.Object);
            _mockClientRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mockClient);

            var allProducts = new Dictionary<string, decimal>
            {
                { "Pain", 0.40m },
                { "Petit Salade Bar", 4m },
                { "Entrée", 3m },
                { "Plat", 6m },
                { "Dessert", 3m }
                
            };
            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allProducts);
            TicketRequestDTO ticketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
                Products = new List<string> { "Pain", "Petit Salade Bar", "Pain", "Entrée", "Plat" }
            };
            var result = await service.GenerateTicketAsync(ticketRequestDTO);

            Assert.Equal(expectedTotalToPay, result.TotalToPay);
        }

        [Theory]
        [InlineData("Interne", "6.9")]
        [InlineData("Prestataire", "8.4")]
        [InlineData("VIP", "0")]
        [InlineData("Stagiaire", "4.4")]
        [InlineData("Visiteur", "14.4")]
        public async Task GenerateTicket_CalculateTotalToPayWithDiscount_OneMenu(String clientType, String expectedTotalToPayStr)
        {
            var mockClient = clientType switch
            {
                "Interne" => MockData.GetMockInterneClient(),
                "Prestataire" => MockData.GetMockPrestataireClient(),
                "VIP" => MockData.GetMockVIPClient(),
                "Stagiaire" => MockData.GetMockStagiaireClient(),
                "Visiteur" => MockData.GetMockVisiteurClient(),
                _ => throw new Exception("Unknown client type")
            };
            decimal expectedTotalToPay = Convert.ToDecimal(expectedTotalToPayStr, CultureInfo.InvariantCulture);

            var service = new TicketService(_mockClientRepository.Object, _mockProductRepository.Object);
            _mockClientRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mockClient);

            var allProducts = new Dictionary<string, decimal>
            {
                { "Pain", 0.40m },
                { "Petit Salade Bar", 4m },
                { "Entrée", 3m },
                { "Plat", 6m },
                { "Dessert", 3m }
            };
            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allProducts);
            TicketRequestDTO ticketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
                Products = new List<string> { "Pain", "Petit Salade Bar", "Pain", "Entrée", "Plat", "Dessert" }
            };
            var result = await service.GenerateTicketAsync(ticketRequestDTO);

            Assert.Equal(expectedTotalToPay, result.TotalToPay);
        }

        [Theory]
        [InlineData("Interne", "12.5")]
        [InlineData("Prestataire", "14")]
        [InlineData("VIP", "0")]
        [InlineData("Stagiaire", "10")]
        [InlineData("Visiteur", "20")]
        public async Task GenerateTicket_CalculateTotalToPayWithDiscount_TwoMenu(String clientType, String expectedTotalToPayStr)
        {
            var mockClient = clientType switch
            {
                "Interne" => MockData.GetMockInterneClient(),
                "Prestataire" => MockData.GetMockPrestataireClient(),
                "VIP" => MockData.GetMockVIPClient(),
                "Stagiaire" => MockData.GetMockStagiaireClient(),
                "Visiteur" => MockData.GetMockVisiteurClient(),
                _ => throw new Exception("Unknown client type")
            };
            decimal expectedTotalToPay = Convert.ToDecimal(expectedTotalToPayStr, CultureInfo.InvariantCulture);

            var service = new TicketService(_mockClientRepository.Object, _mockProductRepository.Object);
            _mockClientRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mockClient);

            var allProducts = new Dictionary<string, decimal>
            {
                { "Pain", 0.40m },
                { "Petit Salade Bar", 4m },
                { "Entrée", 3m },
                { "Plat", 6m },
                { "Dessert", 3m }
            };
            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allProducts);
            TicketRequestDTO ticketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
                Products = new List<string> { "Pain", "Entrée", "Plat", "Dessert", "Pain", "Entrée", "Plat", "Dessert" }
            };
            var result = await service.GenerateTicketAsync(ticketRequestDTO);

            Assert.Equal(expectedTotalToPay, result.TotalToPay);
        }

        [Theory]
        [InlineData("Interne", "22.9")]
        [InlineData("Prestataire", "24.4")]
        [InlineData("VIP", "0")]
        [InlineData("Stagiaire", "20.4")]
        [InlineData("Visiteur", "30.4")]
        public async Task GenerateTicket_TotalToPayExceedBudget(String clientType, String expectedTotalToPayStr)
        {
            var mockClient = clientType switch
            {
                "Interne" => MockData.GetMockInterneClient(),
                "Prestataire" => MockData.GetMockPrestataireClient(),
                "VIP" => MockData.GetMockVIPClient(),
                "Stagiaire" => MockData.GetMockStagiaireClient(),
                "Visiteur" => MockData.GetMockVisiteurClient(),
                _ => throw new Exception("Unknown client type")
            };
            decimal expectedTotalToPay = Convert.ToDecimal(expectedTotalToPayStr, CultureInfo.InvariantCulture);

            var service = new TicketService(_mockClientRepository.Object, _mockProductRepository.Object);
            _mockClientRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mockClient);

            var allProducts = new Dictionary<string, decimal>
            {
                { "Pain", 0.40m },
                { "Petit Salade Bar", 4m },
                { "Entrée", 3m },
                { "Plat", 6m },
                { "Dessert", 3m }
            };
            _mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(allProducts);
            // 3 menus + 1 pain
            TicketRequestDTO ticketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
                Products = new List<string> { "Pain", "Pain", "Pain", "Pain", "Entrée", "Entrée", "Entrée", "Plat", "Plat", "Plat", "Dessert", "Dessert", "Dessert" }
            };
            switch (clientType)
            {
                case "Interne":
                case "VIP":
                    // 3 menus + 1 Pain
                    var result = await service.GenerateTicketAsync(ticketRequestDTO);
                    Assert.Equal(expectedTotalToPay, result.TotalToPay);
                    break;
                case "Prestataire":
                case "Stagiaire":
                case "Visiteur":
                    // 3 menus + 1 Pain
                    var exception = await Assert.ThrowsAsync<BudgetTooLowException>(() => service.GenerateTicketAsync(ticketRequestDTO));
                    break;
            }
        }
    }
}
