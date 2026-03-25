using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Infrastructure.Repo;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Infrastructure.Service;

public class AuthService(AuthRepository repo)
{
    private readonly AuthRepository _repo = repo;

    public async Task<ApiResponseBase> PostAuth_serv(string emailAziendale, string password) {
        try
        {
            // Le credeziali fornite fanno riferimento a un dipendente? Se no, interrompo con un '404' (non trovato).
            Employee? existingEmployee = await _repo.GetAuthByCompanyMailAndPass_repo(emailAziendale, password);
            if (existingEmployee is null) { return ApiResponseFactory.NotFound(); }

            return ApiResponseFactory.Success(existingEmployee);
        } catch (Exception) { throw; }
    }
} 