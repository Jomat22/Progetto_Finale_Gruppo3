using AutoMapper;
using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Infrastructure.Repo;
using store.api.src.Dto.Person;
using store.core.src.Domain.Entity.User;
using store.api.src.Helper;
namespace store.api.src.Infrastructure.Service;

public class EmployeeService(PersonRepository repo, IMapper mapper)
{
    private readonly PersonRepository _repo = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponseBase> GetPerson_serv(string codiceFiscale) {
        try
        {
            Person? person = await _repo.GetPersonByTaxCode_repo(codiceFiscale);
            if (person is null) { return ApiResponseFactory.NotFound(); }

            return ApiResponseFactory.Success(person);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> GetAllPerson_serv() {
        try
        {
            IEnumerable<Person> people = await _repo.GetAllPerson_repo();
            if (!people.Any()) { return ApiResponseFactory.SuccessNoContent(people); }
            
            return ApiResponseFactory.Success(people); 
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PostPerson_serv(PersonCreateRequest request) {
        try
        {
            Person? existingPerson = await _repo.GetPersonByTaxCode_repo(request.CodiceFiscale);
            if (existingPerson is not null) { return ApiResponseFactory.Conflict(); }

            Person person = _mapper.Map<Person>(request);
            DataHelper.InputDataFormatUppercase(person);
            DataHelper.InputDataAddTimestamp(person);

            int numRows = await _repo.PostPerson_repo(person);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(person); }

            return ApiResponseFactory.Success(person);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PutPerson_serv(PersonUpdateRequest request) {
        try
        {
            Person? existingPerson = await _repo.GetPersonByTaxCode_repo(request.CodiceFiscale);
            if (existingPerson is null) { return ApiResponseFactory.NotFound(); }

            _mapper.Map(request, existingPerson);
            DataHelper.InputDataFormatUppercase(existingPerson);
            DataHelper.InputDataUpdateTimestamp(existingPerson);
            
            int numRows = await _repo.PutPerson_repo(); // Non serve passarlo perchè ha già tracciato i cambiamenti i nmemoria. Chiamo solo il 'SaveChanges'
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(existingPerson); }

            return ApiResponseFactory.Success(existingPerson);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> DeletePerson_serv(string codiceFiscale) {
        try
        {
            Person? existingPerson = await _repo.GetPersonByTaxCode_repo(codiceFiscale);
            if (existingPerson is null) { return ApiResponseFactory.NotFound(); }

            int numRows = await _repo.DeletePerson_repo(existingPerson);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(existingPerson); }

            return ApiResponseFactory.Success(existingPerson);
        } catch (Exception) { throw; }
    }
} 