using AutoMapper;
using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Infrastructure.Repo;
using store.api.src.Dto.Employee;
using store.core.src.Domain.Entity.User;
using store.api.src.Helper;
namespace store.api.src.Infrastructure.Service;

public class EmployeeService(EmployeeRepository repo, PersonRepository repoPerson, /* ClientRepository  _repoClient, */ IMapper mapper)
{
    private readonly EmployeeRepository _repo = repo;
    private readonly PersonRepository _repoPerson = repoPerson;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponseBase> GetEmployee_serv(string codiceMeccanografico) {
        try
        {
            Employee? employee = await _repo.GetEmployeeByMCode_repo(codiceMeccanografico);
            if (employee is null) { return ApiResponseFactory.NotFound(); }

            return ApiResponseFactory.Success(employee);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> GetAllEmployee_serv() {
        try
        {
            IEnumerable<Employee> employee = await _repo.GetAllEmployee_repo();
            if (!employee.Any()) { return ApiResponseFactory.SuccessNoContent(employee); }
            
            return ApiResponseFactory.Success(employee); 
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PostEmployee_serv(EmployeeCreateRequest request) {
        try
        {
            // Esiste la persona? Se no, interrompo con un '404' (non trovato).
            Person? existingPerson = await _repoPerson.GetPersonById_repo(request.PersonId);
            if (existingPerson is null) { return ApiResponseFactory.NotFound(); }

            // Id della persona già associato ad un dipendente? Se si, interrompo con un '409' (conflitto)
            Employee? existingPersonInEmployee = await _repo.GetEmployeeByPersonId_repo(request.PersonId);
            if (existingPersonInEmployee is not null) { return ApiResponseFactory.Conflict(); }
            
            // Codice meccanografico già associato ad un dipendente? Se si, interrompo con un '409' (conflitto)
            Employee? existingEmployee = await _repo.GetEmployeeByMCode_repo(request.CodiceMeccanografico);
            if (existingEmployee is not null) { return ApiResponseFactory.Conflict(); }
            
            // Email aziendale già associata ad un dipendente? Se si, interrompo con un '409' (conflitto)
            existingEmployee = await _repo.GetEmployeeByCompanyEmail_repo(request.EmailAziendale);
            if (existingEmployee is not null) { return ApiResponseFactory.Conflict(); }

            Employee employee = _mapper.Map<Employee>(request);
            DataHelper.InputDataFormatUppercase(employee);
            DataHelper.InputDataAddTimestamp(employee);

            int numRows = await _repo.PostEmployee_repo(employee);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(employee); }

            return ApiResponseFactory.Success(employee);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PutEmployee_serv(EmployeeUpdateRequest request) {
        try
        {
            Employee? existingEmployee = await _repo.GetEmployeeByMCode_repo(request.CodiceMeccanografico, false);
            if (existingEmployee is null) { return ApiResponseFactory.NotFound(); }

            // Se è stata aggiornato il 'PersonId' e quindi non ha lo stesso 'PersonId' del dipendente recuperato dal db... 
            if (request.PersonId != existingEmployee.PersonId)
            {
                // Effettuo un get per controllare se il 'PersonId' non sia già in uso
                Employee? existingEmployeeWithSamePersonId = await _repo.GetEmployeeByPersonId_repo(request.PersonId);

                // Se cosi fosse, allora interrompo con un '409' (conflitto)
                if (existingEmployeeWithSamePersonId is not null) { return ApiResponseFactory.Conflict();  }
            }

            // Se è stata aggiornata l'email aziendale... 
            // (controllo se la richiesta in entrata "NON" abbia la stessa email del dipendente recuperato dal db)
            if (!string.Equals(request.EmailAziendale, existingEmployee.EmailAziendale, StringComparison.OrdinalIgnoreCase))
            {
                // Se cosi fosse, effettuo un get per controllare se la nuova email inserita non sia già in uso
                Employee? existingEmployeeWithSameCEmail = await _repo.GetEmployeeByCompanyEmail_repo(request.EmailAziendale);

                // Se cosi fosse, allora interrompo con un '409' (conflitto)
                if (existingEmployeeWithSameCEmail is not null) { return ApiResponseFactory.Conflict();  }
            }

            _mapper.Map(request, existingEmployee);
            DataHelper.InputDataFormatUppercase(existingEmployee);
            DataHelper.InputDataUpdateTimestamp(existingEmployee);
            
            int numRows = await _repo.PutEmployee_repo(); // Non serve passarlo perchè ha già tracciato i cambiamenti i nmemoria. Chiamo solo il 'SaveChanges'
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(existingEmployee); }

            return ApiResponseFactory.Success(existingEmployee);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> DeleteEmployee_serv(string codiceMeccanografico) {
        try
        {
            Employee? existingEmployee = await _repo.GetEmployeeByMCode_repo(codiceMeccanografico);
            if (existingEmployee is null) { return ApiResponseFactory.NotFound(); }

            int numRows = await _repo.DeleteEmployee_repo(existingEmployee);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(existingEmployee); }

            return ApiResponseFactory.Success(existingEmployee);
        } catch (Exception) { throw; }
    }
} 