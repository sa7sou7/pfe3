using Microsoft.EntityFrameworkCore;
using pfe3.Core.Entities;
using pfe3.Core.Interfaces;
using pfe3.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pfe3.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly BusinessDbContext _context;

        public ClientRepository(BusinessDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Clients>> GetAllClientsAsync()
        {
            return await _context.Clients.Include(c => c.Contactes).ToListAsync();
        }

        public async Task<Clients?> GetClientByIdAsync(int id)
        {
            return await _context.Clients.Include(c => c.Contactes)
                                         .FirstOrDefaultAsync(c => c.ClientId == id);
        }

        public async Task AddClientAsync(Clients client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(Clients client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
