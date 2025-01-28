using Cantine.Application.Services;
using Cantine.Application.Services.IServices;
using CantineWebAPI.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cantine.Application.Models;

namespace CantineWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("order")]
        public async Task<IActionResult> GenerateTicket([FromBody] TicketRequestDTO request)
        {
            TicketRequestValidator validator = new TicketRequestValidator();
            ValidationResult results = validator.Validate(request);
            if (!results.IsValid)
            {
                return BadRequest(results.Errors);
            }
            try
            {
                var ticket = await _ticketService.GenerateTicketAsync(request);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to order a ticket : {ex.Message}");
            }
        }
    }
}
