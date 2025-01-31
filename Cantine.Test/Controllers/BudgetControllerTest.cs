using CantineWebAPI.Controllers;
using Cantine.Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cantine.Application.Models;
using Cantine.Application.Errors;
using Cantine.Application.Services;

namespace Cantine.Test.Controllers
{
    public class BudgetControllerTest
    {
        private readonly Mock<IBudgetService> _mockBudgetService;
        private readonly BudgetController _controller;
        public BudgetControllerTest()
        {
            _mockBudgetService = new Mock<IBudgetService>();
            _controller = new BudgetController(_mockBudgetService.Object);
        }
        [Fact]
        public async Task AddBudget_EmptyClientID_ReturnsBadRequest()
        {
            var mockAddBudgetDTO = new AddBudgetDTO()
            {
                ClientId = Guid.Empty,
                Amount = 15m
            };
            _mockBudgetService.Setup(service => service.AddBudgetAsync(mockAddBudgetDTO)).Returns(Task.CompletedTask);

            var result = await _controller.AddBudget(mockAddBudgetDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }
        [Fact]
        public async Task AddBudget_ClientIdMissing_ReturnsBadRequest()
        {
            var mockAddBudgetDTO = new AddBudgetDTO()
            {
                Amount = 15m
            };
            _mockBudgetService.Setup(service => service.AddBudgetAsync(mockAddBudgetDTO)).Returns(Task.CompletedTask);

            var result = await _controller.AddBudget(mockAddBudgetDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }
        [Fact]
        public async Task AddBudget_AmountMissing_ReturnsBadRequest()
        {
            var mockClient = MockData.GetMockVIPClient();
            var mockAddBudgetDTO = new AddBudgetDTO()
            {
                ClientId = mockClient.Id
            };
            _mockBudgetService.Setup(service => service.AddBudgetAsync(mockAddBudgetDTO)).Returns(Task.CompletedTask);

            var result = await _controller.AddBudget(mockAddBudgetDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }
        [Fact]
        public async Task AddBudget_NegativeAmount_ReturnsBadRequest()
        {
            var mockClient = MockData.GetMockVIPClient();
            var mockAddBudgetDTO = new AddBudgetDTO()
            {
                ClientId = mockClient.Id,
                Amount = -15m
            };
            _mockBudgetService.Setup(service => service.AddBudgetAsync(mockAddBudgetDTO)).Returns(Task.CompletedTask);

            var result = await _controller.AddBudget(mockAddBudgetDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }

        [Fact]
        public async Task AddBudget_TaskSuccess_ReturnsOk()
        {
            var mockClient = MockData.GetMockVIPClient();
            var mockAddBudgetDTO = new AddBudgetDTO()
            {
                ClientId = mockClient.Id,
                Amount = 15m
            };
            _mockBudgetService.Setup(service => service.AddBudgetAsync(mockAddBudgetDTO)).Returns(Task.CompletedTask);

            var result = await _controller.AddBudget(mockAddBudgetDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
        [Fact]
        public async Task AddBudget_ServiceThrowsException_ReturnsInternalServerError()
        {
            var mockClient = MockData.GetMockVIPClient();
            var addBudgetDTO = new AddBudgetDTO()
            {
                ClientId = mockClient.Id,
                Amount = 15m
            };
            _mockBudgetService.Setup(service => service.AddBudgetAsync(addBudgetDTO)).ThrowsAsync(new Exception("An error occured."));

            var exception = await Assert.ThrowsAsync<Exception>(() => _controller.AddBudget(addBudgetDTO));
            Assert.Equal("An error occured.", exception.Message);
        }
    }
}
