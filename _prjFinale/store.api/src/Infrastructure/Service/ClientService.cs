using AutoMapper;
using store.api.src.Common;
using store.api.src.Dto.Client;
using store.api.src.Factory;
using store.api.src.Helper;
using store.api.src.Infrastructure.Repo;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Infrastructure.Service;

public class ClientService(ClientRepository repo, PersonRepository repoPerson, /* ClientRepository  _repoClient, */ IMapper mapper)
{
    private readonly ClientRepository _repo = repo;
    private readonly PersonRepository _repoPerson = repoPerson;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponseBase> GetClient_serv(string codiceCliente) {
        try
        {
            Client? client = await _repo.GetClientByCCode_repo(codiceCliente);
            if (client is null) { return ApiResponseFactory.NotFound(); }

            return ApiResponseFactory.Success(client);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> GetAllClient_serv() {
        try
        {
            IEnumerable<Client> client = await _repo.GetAllClient_repo();
            if (!client.Any()) { return ApiResponseFactory.SuccessNoContent(client); }
            
            return ApiResponseFactory.Success(client); 
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PostClient_serv(ClientCreateRequest request) {
        try
        {
            // Esiste la persona? Se no, interrompo con un '404' (non trovato).
            Person? existingPerson = await _repoPerson.GetPersonById_repo(request.PersonId);
            if (existingPerson is null) { return ApiResponseFactory.NotFound(); }
            
            // Id della persona già associato ad un dipendente? Se si, interrompo con un '409' (conflitto)
            Client? existingPersonInClient = await _repo.GetClientByPersonId_repo(request.PersonId);
            if (existingPersonInClient is not null) { return ApiResponseFactory.Conflict(); }

            // Codice cliente già associato ad un cliente? Se si, interrompo con un '409' (conflitto)
            Client? existingClient = await _repo.GetClientByCCode_repo(request.CodiceCliente);
            if (existingClient is not null) { return ApiResponseFactory.Conflict(); }

            Client client = _mapper.Map<Client>(request);
            DataHelper.InputDataFormatUppercase(client);
            DataHelper.InputDataAddTimestamp(client);

            int numRows = await _repo.PostClient_repo(client);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(client); }

            return ApiResponseFactory.Success(client);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PutClient_serv(ClientUpdateRequest request) {
        try
        {
            Client? existingClient = await _repo.GetClientByCCode_repo(request.CodiceCliente, false);
            if (existingClient is null) { return ApiResponseFactory.NotFound(); }

            // Se è stata aggiornato il 'PersonId' e quindi non ha lo stesso 'PersonId' del cliente recuperato dal db... 
            if (request.PersonId != existingClient.PersonId)
            {
                // Effettuo un get per controllare se il 'PersonId' non sia già in uso
                Client? existingClientWithSamePersonId = await _repo.GetClientByPersonId_repo(request.PersonId);

                // Se cosi fosse, allora interrompo con un '409' (conflitto)
                if (existingClientWithSamePersonId is not null) { return ApiResponseFactory.Conflict();  }
            }

            _mapper.Map(request, existingClient);
            DataHelper.InputDataFormatUppercase(existingClient);
            DataHelper.InputDataUpdateTimestamp(existingClient);
            
            int numRows = await _repo.PutClient_repo(); // Non serve passarlo perchè ha già tracciato i cambiamenti i nmemoria. Chiamo solo il 'SaveChanges'
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(existingClient); }

            return ApiResponseFactory.Success(existingClient);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> DeleteClient_serv(string codiceCliente) {
        try
        {
            Client? existingClient = await _repo.GetClientByCCode_repo(codiceCliente, false);
            if (existingClient is null) { return ApiResponseFactory.NotFound(); }

            int numRows = await _repo.DeleteClient_repo(existingClient);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(existingClient); }

            return ApiResponseFactory.Success(existingClient);
        } catch (Exception) { throw; }
    }
} 