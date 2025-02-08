using Microsoft.AspNetCore.Mvc;
using pfe3.Application.Dtos;
using pfe3.Core.Entities;
using pfe3.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pfe3.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            if (clients == null || !clients.Any())
            {
                return NotFound("No clients found.");
            }

            // Return clients along with their contacts
            var clientDtos = clients.Select(client => new
            {
                client.ClientId,
                client.Nom,
                client.Prenom,
                client.Adresse,
                client.Cin,
                client.Matricule,
                client.Statut,
                Contactes = client.Contactes.Select(c => new
                {
                    c.Email,
                    c.Phone
                }).ToList()
            }).ToList();

            return Ok(clientDtos);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound("No client found.");
            }

            // Return client along with its contacts
            var clientDto = new
            {
                client.ClientId,
                client.Nom,
                client.Prenom,
                client.Adresse,
                client.Cin,
                client.Matricule,
                client.Statut,
                Contactes = client.Contactes.Select(c => new
                {
                    c.Email,
                    c.Phone
                }).ToList()
            };

            return Ok(clientDto);
        }


        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientsDto clientDto)
        {
            if (clientDto == null || clientDto.Contactes == null || clientDto.Contactes.Count == 0)
            {
                return BadRequest("Client and Contacts are required.");
            }

            var client = new Clients
            {
                Nom = clientDto.Nom,
                Prenom = clientDto.Prenom,
                Adresse = clientDto.Adresse,
                Cin = clientDto.Cin,
                Matricule = clientDto.Matricule,
                Statut = clientDto.Statut,
                Contactes = clientDto.Contactes.Select(c => new Contactes
                {
                    Email = c.Email,
                    Phone = c.Phone
                }).ToList()
            };

            await _clientService.AddClientAsync(client);

            return CreatedAtAction(nameof(GetClient), new { id = client.ClientId }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientsDto clientDto)
        {
     

            // Check if the client exists
            var existingClient = await _clientService.GetClientByIdAsync(id);
            if (existingClient == null)
            {
                return NotFound($"Client with ID {id} not found.");
            }

            // Update the client properties
            existingClient.Nom = clientDto.Nom;
            existingClient.Prenom = clientDto.Prenom;
            existingClient.Adresse = clientDto.Adresse;
            existingClient.Cin = clientDto.Cin;
            existingClient.Matricule = clientDto.Matricule;
            existingClient.Statut = clientDto.Statut;

            // Update or remove contactes
            existingClient.Contactes = clientDto.Contactes.Select(c => new Contactes
            {
                Email = c.Email,
                Phone = c.Phone
            }).ToList();

            // Call the service to update the client in the database
            await _clientService.UpdateClientAsync(existingClient);

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }
    }
}
