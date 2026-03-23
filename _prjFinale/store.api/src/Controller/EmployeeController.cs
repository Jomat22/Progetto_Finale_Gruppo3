using Microsoft.AspNetCore.Mvc;
using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Infrastructure.Service;
using store.api.src.Dto.Employee;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Controller;

[ApiController]
[Route("api/[Controller]")]
public class EmployeeController(EmployeeService serv) : ControllerBase
{
    private readonly EmployeeService _serv = serv;

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{codiceMeccanografico}")]
    public async Task<IActionResult> GetEmployee_cont([FromRoute] string codiceMeccanografico)
    {
        try
        {
            EmployeeGetByRequest request = new() { CodiceMeccanografico = codiceMeccanografico };
            if(codiceMeccanografico == "{codiceMeccanografico}" || !TryValidateModel(request)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }

            ApiResponseBase response = await _serv.GetEmployee_serv(request.CodiceMeccanografico);

            return response switch
            {
                ApiResponse_Success<Employee> success => Ok(success),
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
    public async Task<IActionResult> GetAllEmployee_cont()
    {
        try 
        {
            ApiResponseBase response = await _serv.GetAllEmployee_serv();

            return response switch
            {
                ApiResponse_Success<IEnumerable<Employee>> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> PostEmployee_cont([FromBody] EmployeeCreateRequest request)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState));
            ApiResponseBase response = await _serv.PostEmployee_serv(request);
            
            return response switch
            {
                ApiResponse_Success<Employee> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut]
    public async Task<IActionResult> PutEmployee_cont([FromBody] EmployeeUpdateRequest request)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState));
            ApiResponseBase response = await _serv.PutEmployee_serv(request);
            
            return response switch
            {
                ApiResponse_Success<Employee> success => Ok(success),
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
    [HttpDelete("{codiceMeccanografico}")]
    public async Task<IActionResult> DeleteEmployee_cont([FromRoute] string codiceMeccanografico)
    {
        try
        {
            EmployeeDeleteRequest request = new() { CodiceMeccanografico = codiceMeccanografico };
            if(codiceMeccanografico == "{codiceMeccanografico}" || !TryValidateModel(request)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }

            ApiResponseBase response = await _serv.DeleteEmployee_serv(request.CodiceMeccanografico);
            
            return response switch
            {
                ApiResponse_Success<Employee> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
}