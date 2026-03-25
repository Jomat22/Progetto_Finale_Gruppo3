using Microsoft.AspNetCore.Mvc;
using store.api.src.Common;
using store.api.src.Dto.Auth;
using store.api.src.Factory;
using store.api.src.Infrastructure.Service;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService serv) : ControllerBase
{
    private readonly AuthService _serv = serv;

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> PostAuth_cont([FromBody] AuthLoginRequest authLoginRequest)
    {
        try
        {
            if (!ModelState.IsValid) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }
            ApiResponseBase response = await _serv.PostAuth_serv(authLoginRequest.EmailAziendale, authLoginRequest.Password);

            return response switch
            {
                ApiResponse_Success<Employee> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError())
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
}