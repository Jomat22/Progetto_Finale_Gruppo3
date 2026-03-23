using Microsoft.AspNetCore.Mvc;
using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Infrastructure.Service;
using store.api.src.Dto.Person;
using store.core.src.Domain.Entity.User;
namespace store.api.src.Controller;

[ApiController]
[Route("api/[Controller]")]
public class PersonController(PersonService serv) : ControllerBase
{
    private readonly PersonService _serv = serv;

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{codiceFiscale}")]
    public async Task<IActionResult> GetClass_cont([FromRoute] string codiceFiscale)
    {
        try
        {
            PersonGetByRequest request = new() { CodiceFiscale = codiceFiscale };
            if(codiceFiscale == "{codiceFiscale}" || !TryValidateModel(request)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }

            ApiResponseBase response = await _serv.GetPerson_serv(request.CodiceFiscale);

            return response switch
            {
                ApiResponse_Success<Person> success => Ok(success),
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
    public async Task<IActionResult> GetAllPerson_cont()
    {
        try 
        {
            ApiResponseBase response = await _serv.GetAllPerson_serv();

            return response switch
            {
                ApiResponse_Success<IEnumerable<Person>> success => Ok(success),
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
    public async Task<IActionResult> PostPerson_cont([FromBody] PersonCreateRequest request)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState));
            ApiResponseBase response = await _serv.PostPerson_serv(request);
            
            return response switch
            {
                ApiResponse_Success<Person> success => Ok(success),
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
    public async Task<IActionResult> PutPerson_cont([FromBody] PersonUpdateRequest request)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState));
            ApiResponseBase response = await _serv.PutPerson_serv(request);
            
            return response switch
            {
                ApiResponse_Success<Person> success => Ok(success),
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
    [HttpDelete("{codiceFiscale}")]
    public async Task<IActionResult> DeletePerson_cont([FromRoute] string codiceFiscale)
    {
        try
        {
            PersonDeleteRequest request = new() { CodiceFiscale = codiceFiscale };
            if(codiceFiscale == "{codiceFiscale}" || !TryValidateModel(request)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }

            ApiResponseBase response = await _serv.DeletePerson_serv(request.CodiceFiscale);
            
            return response switch
            {
                ApiResponse_Success<Person> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
}