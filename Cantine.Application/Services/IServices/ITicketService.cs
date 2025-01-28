using Cantine.Application.Models;

namespace Cantine.Application.Services.IServices
{
    public interface ITicketService
    {
        Task<TicketDTO> GenerateTicketAsync(TicketRequestDTO ticketRequestDTO);
    }
}
