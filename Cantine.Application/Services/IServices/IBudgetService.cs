
using Cantine.Application.Models;

namespace Cantine.Application.Services.IServices
{
    public interface IBudgetService
    {
        Task AddBudgetAsync(AddBudgetDTO addBudgetDTO);
    }
}
