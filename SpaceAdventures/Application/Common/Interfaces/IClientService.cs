using SpaceAdventures.Application.Common.Commands.Clients;
using SpaceAdventures.Application.Common.Models;
using SpaceAdventures.Application.Common.Queries.Clients;
using SpaceAdventures.Application.Common.Queries.Clients.GetClientsWithPagination;

namespace SpaceAdventures.Application.Common.Interfaces;

public interface IClientService
{
    Task<ClientsVm> GetAllClients(CancellationToken cancellation = default);
    Task<PaginatedList<ClientDto>> GetAllClientsWithPagination(int pageNumber, int pageSize, CancellationToken cancellation = default);
    Task<ClientDto> GetClientById(int clientId, CancellationToken cancellation = default);
    Task<ClientDto> GetClientByIdUser(int id, CancellationToken cancellation = default);
    Task<ClientDto> GetClientByEmail(string email, CancellationToken cancellation = default);   
    Task<ClientDto> CreateClient(ClientInput clientInput, CancellationToken cancellation = default);
    Task<ClientDto> UpdateClient(ClientInput clientInput, CancellationToken cancellation = default);
    Task DeleteClient(int clientId, CancellationToken cancellation = default);
    Task<bool> ClientExists(string email);
    bool ClientExists(int? id, ClientInput clientInput);
}