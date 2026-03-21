using store.api._.Common;
using store.api._.Factory;
using store.api._.Infrastructure.Repo;
namespace store.api._.Infrastructure.Service;

public class PersonService(PersonRepository repo)
{
    private readonly PersonRepository _repo = repo;
    
    public async Task<ApiResponseBase> GetPerson_serv() { 
        return ApiResponseFactory.SuccessNoChanges(string.Empty); 
    }
    
    public async Task<ApiResponseBase> GetAllPerson_serv() { 
        return ApiResponseFactory.SuccessNoChanges(string.Empty); 
    }
    
    public async Task<ApiResponseBase> PostPerson_serv() { 
        return ApiResponseFactory.SuccessNoChanges(string.Empty); 
    }
    
    public async Task<ApiResponseBase> PutPerson_serv() { 
        return ApiResponseFactory.SuccessNoChanges(string.Empty); 
    }
    
    public async Task<ApiResponseBase> DeletePerson_serv() { 
        return ApiResponseFactory.SuccessNoChanges(string.Empty); 
    }
} 