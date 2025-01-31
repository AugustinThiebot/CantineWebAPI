using Cantine.Application.Errors;
using Cantine.Application.Models;
using Cantine.Application.Services.IServices;
using Cantine.DataAccess.Repository;
using Cantine.DataAccess.Repository.IRepository;

namespace Cantine.Application.Services
{
    public class BudgetService: IBudgetService
    {
        private readonly IClientRepository _clientRepository;
        public BudgetService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task AddBudgetAsync(AddBudgetDTO addbudgetDTO)
        {
            Guid clientId = addbudgetDTO.ClientId;
            decimal amount = addbudgetDTO.Amount;
            if (amount <= 0) {
                throw new Exception("Amount can not be negative.");
            }
            try
            {
                var client = await _clientRepository.GetByIdAsync(clientId);
                if (client == null)
                {
                    throw new ClientNotFoundException(clientId);
                }
                client.Budget += amount;
                await _clientRepository.UpdateAsync(client);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
