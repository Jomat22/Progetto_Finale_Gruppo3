using Microsoft.AspNetCore.Mvc;
using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Infrastructure.Service;
using store.api.src.Dto.Client;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Controller;

[ApiController]
[Route("api/[Controller]")]
public class ClientController(ClientService serv) : ControllerBase
{
    private readonly ClientService _serv = serv;

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{codiceCliente}")]
    public async Task<IActionResult> GetClient_cont([FromRoute] string codiceCliente)
    {
        try
        {
            ClientGetByRequest request = new() { CodiceCliente = codiceCliente };
            if(codiceCliente == "{codiceCliente}" || !TryValidateModel(request)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }

            ApiResponseBase response = await _serv.GetClient_serv(request.CodiceCliente);

            return response switch
            {
                ApiResponse_Success<Client> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAllClient_cont()
    {
        try 
        {
            ApiResponseBase response = await _serv.GetAllClient_serv();

            return response switch
            {
                ApiResponse_Success<IEnumerable<Client>> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> PostClient_cont([FromBody] ClientCreateRequest request)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState));
            ApiResponseBase response = await _serv.PostClient_serv(request);
            
            return response switch
            {
                ApiResponse_Success<Client> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<IActionResult> PutClient_cont([FromBody] ClientUpdateRequest request)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState));
            ApiResponseBase response = await _serv.PutClient_serv(request);
            
            return response switch
            {
                ApiResponse_Success<Client> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{codiceCliente}")]
    public async Task<IActionResult> DeleteClient_cont([FromRoute] string codiceCliente)
    {
        try
        {
            ClientDeleteRequest request = new() { CodiceCliente = codiceCliente };
            if(codiceCliente == "{codiceCliente}" || !TryValidateModel(request)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }

            ApiResponseBase response = await _serv.DeleteClient_serv(request.CodiceCliente);
            
            return response switch
            {
                ApiResponse_Success<Client> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
}