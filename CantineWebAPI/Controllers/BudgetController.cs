using Cantine.Application.Models;
using Cantine.Application.Services;
using Cantine.Application.Services.IServices;
using CantineWebAPI.Validators;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CantineWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;
        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBudget([FromBody] AddBudgetDTO dto)
        {
            BudgetValidator validator = new BudgetValidator();
            ValidationResult results = validator.Validate(dto);
            if (!results.IsValid)
            {
                return BadRequest(results.Errors);
            }
            await _budgetService.AddBudgetAsync(dto);
            return Ok("Amount successfully added.");
        }


    }
}
