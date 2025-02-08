using pfe3.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pfe3.Core.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Clients>> GetAllClientsAsync();
        Task<Clients?> GetClientByIdAsync(int id);
        Task AddClientAsync(Clients client);
        Task UpdateClientAsync(Clients client);
        Task DeleteClientAsync(int id);
    }
}
