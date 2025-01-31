using Cantine.DataAccess.Repository.IRepository;
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

namespace Cantine.Test.Controllers
{
    public class TicketControllerTest
    {
        private readonly Mock<ITicketService> _mockTicketService;
        private readonly TicketController _controller;
        public TicketControllerTest()
        {
            _mockTicketService = new Mock<ITicketService>();
            _controller = new TicketController(_mockTicketService.Object);
        }

        [Fact]
        public async Task OrderTicket_EmptyClientId_ReturnsBadRequest()
        {
            var mockListProducts = MockData.GetMockListProducts();
            var mockTicketRequestDTO = new TicketRequestDTO
            {
                ClientId = Guid.Empty,
                Products = mockListProducts
            };
            _mockTicketService.Setup(service => service.GenerateTicketAsync(It.IsAny<TicketRequestDTO>())).ReturnsAsync(new TicketDTO());

            var result = await _controller.GenerateTicket(mockTicketRequestDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }
        [Fact]
        public async Task OrderTicket_MissingClientId_ReturnsBadRequest()
        {
            var mockListProducts = MockData.GetMockListProducts();
            var mockTicketRequestDTO = new TicketRequestDTO
            {
                Products = mockListProducts
            };
            _mockTicketService.Setup(service => service.GenerateTicketAsync(It.IsAny<TicketRequestDTO>())).ReturnsAsync(new TicketDTO());

            var result = await _controller.GenerateTicket(mockTicketRequestDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }
        [Fact]
        public async Task OrderTicket_MissingProducts_ReturnsBadRequest()
        {
            var mockClient = MockData.GetMockVIPClient();
            var mockTicketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
            };
            _mockTicketService.Setup(service => service.GenerateTicketAsync(It.IsAny<TicketRequestDTO>())).ReturnsAsync(new TicketDTO());

            var result = await _controller.GenerateTicket(mockTicketRequestDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }
        [Fact]
        public async Task OrderTicket_EmptyProductsList_ReturnsBadRequest()
        {
            var mockClient = MockData.GetMockVIPClient();
            var mockTicketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
                Products = new List<string>()
            };
            _mockTicketService.Setup(service => service.GenerateTicketAsync(It.IsAny<TicketRequestDTO>())).ReturnsAsync(new TicketDTO());

            var result = await _controller.GenerateTicket(mockTicketRequestDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }
        [Fact]
        public async Task OrderTicket_ProductsListHasEmptyName_ReturnsBadRequest()
        {
            var mockClient = MockData.GetMockVIPClient();
            var mockTicketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
                Products = new List<string> { "Pain", ""}
            };
            _mockTicketService.Setup(service => service.GenerateTicketAsync(It.IsAny<TicketRequestDTO>())).ReturnsAsync(new TicketDTO());

            var result = await _controller.GenerateTicket(mockTicketRequestDTO);

            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, okResult.StatusCode);
        }

        [Fact]
        public async Task OrderTicket_TaskSuccess_ReturnsOk()
        {
            var mockClient = MockData.GetMockVIPClient();
            var mockListProducts = MockData.GetMockListProducts();
            var mockTicketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
                Products = mockListProducts
            };
            _mockTicketService.Setup(service => service.GenerateTicketAsync(It.IsAny<TicketRequestDTO>())).ReturnsAsync(new TicketDTO());

            var result = await _controller.GenerateTicket(mockTicketRequestDTO);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }
        [Fact]
        public async Task OrderTicket_ServiceThrowsException_ReturnsInternalServerError()
        {
            var mockClient = MockData.GetMockVIPClient();
            var mockListProducts = MockData.GetMockListProducts();
            var ticketRequestDTO = new TicketRequestDTO
            {
                ClientId = mockClient.Id,
                Products = mockListProducts
            };
            _mockTicketService.Setup(service => service.GenerateTicketAsync(It.IsAny<TicketRequestDTO>())).ThrowsAsync(new Exception("An error occured."));

            var exception = await Assert.ThrowsAsync<Exception>(() => _controller.GenerateTicket(ticketRequestDTO));
        }
    }
}
