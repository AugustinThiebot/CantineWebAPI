using Cantine.DataAccess.Repository.IRepository;
using Cantine.Domain;
using Cantine.Application.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cantine.Application.Models;
using Cantine.Application.Errors;

namespace Cantine.Test.Services
{
    public class BudgetServiceTest
    {
        private readonly Mock<IClientRepository> _mockClientRepository;
        
        public BudgetServiceTest()
        {
            _mockClientRepository = new Mock<IClientRepository>();
        }

        [Fact]
        public async Task AddBudget_ToExistingClient_ValidAmount()
        {
            var mockClient = MockData.GetMockVIPClient();
            _mockClientRepository.Setup(repo => repo.GetByIdAsync(mockClient.Id)).ReturnsAsync(mockClient);
            _mockClientRepository.Setup(repo => repo.UpdateAsync(mockClient)).Callback<Client>(client =>
            {
                mockClient = client;
            });
            var budgetService = new BudgetService(_mockClientRepository.Object);
            AddBudgetDTO addBudgetDTO = new AddBudgetDTO
            {
                ClientId = mockClient.Id,
                Amount = 15m
            };
            await budgetService.AddBudgetAsync(addBudgetDTO);

            Assert.Equal(35m, mockClient.Budget);
        }
        [Fact]
        public async Task AddBudget_ToExistingClient_InvalidAmount()
        {
            var mockClient = MockData.GetMockVIPClient();
            _mockClientRepository.Setup(repo => repo.GetByIdAsync(mockClient.Id)).ReturnsAsync(mockClient);
            _mockClientRepository.Setup(repo => repo.UpdateAsync(mockClient)).Callback<Client>(client =>
            {
                mockClient = client;
            });
            var budgetService = new BudgetService(_mockClientRepository.Object);
            AddBudgetDTO addBudgetDTO = new AddBudgetDTO
            {
                ClientId = mockClient.Id,
                Amount = -15m
            };
            var exception = await Assert.ThrowsAsync<Exception>(() => budgetService.AddBudgetAsync(addBudgetDTO));

            Assert.Equal("Amount can not be negative.", exception.Message);
            Assert.Equal(20m, mockClient.Budget);
        }
        [Fact]
        public async Task AddBudget_ToUnknownClient_ValidAmount()
        {
            var mockClient = MockData.GetMockVIPClient();
            _mockClientRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Client)null);
            var budgetService = new BudgetService(_mockClientRepository.Object);
            AddBudgetDTO addBudgetDTO = new AddBudgetDTO
            {
                ClientId = mockClient.Id,
                Amount = 15m
            };
            var exception = await Assert.ThrowsAsync<ClientNotFoundException>(() => budgetService.AddBudgetAsync(addBudgetDTO));
            Assert.StartsWith("Unknown client : ", exception.Message);
        }
    }
}
