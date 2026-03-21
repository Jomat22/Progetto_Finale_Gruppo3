using store.api._.Common;
using store.api._.Factory;
using store.api._.Infrastructure.Repo;
using store.core._.Application.Dto.Person;
using store.core._.Domain.Entity.User;
namespace store.api._.Infrastructure.Service;

public class PersonService(PersonRepository repo)
{
    private readonly PersonRepository _repo = repo;
    
    public async Task<ApiResponseBase> GetPerson_serv(string codiceFiscale) {
        try
        {
            Person? person = await _repo.GetPerson_repo(codiceFiscale);
            if (person is null) return ApiResponseFactory.NotFound();

            return ApiResponseFactory.Success(person);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> GetAllPerson_serv() {
        IEnumerable<Person> people = await _repo.GetAllPerson_repo();
        if (!people.Any()) return ApiResponseFactory.SuccessNoContent(people);
        return ApiResponseFactory.Success(people); 
    }
    
    public async Task<ApiResponseBase> PostPerson_serv(PersonCreateRequest request) {
        try
        {
            Person? existingPerson = await _repo.GetPerson_repo(request.Email);
            if (existingPerson is not null) return ApiResponseFactory.Conflict();

            

            return ApiResponseFactory.SuccessNoChanges(string.Empty);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PutPerson_serv() {
        try
        {
            return ApiResponseFactory.SuccessNoChanges(string.Empty);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> DeletePerson_serv() {
        try
        {
            return ApiResponseFactory.SuccessNoChanges(string.Empty);
        } catch (Exception) { throw; }
    }
} 