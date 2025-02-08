using pfe3.Core.Entities;
using pfe3.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pfe3.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IEnumerable<Clients>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllClientsAsync();
        }

        public async Task<Clients?> GetClientByIdAsync(int id)
        {
            return await _clientRepository.GetClientByIdAsync(id);
        }

        public async Task AddClientAsync(Clients client)
        {
            await _clientRepository.AddClientAsync(client);
        }

        public async Task UpdateClientAsync(Clients client)
        {
            await _clientRepository.UpdateClientAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteClientAsync(id);
        }
    }
}
