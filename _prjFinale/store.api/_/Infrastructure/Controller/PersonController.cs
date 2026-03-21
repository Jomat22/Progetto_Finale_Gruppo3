using Microsoft.AspNetCore.Mvc;
using store.api._.Common;
using store.api._.Dto.Person;
using store.api._.Factory;
using store.api._.Infrastructure.Service;
using store.core._.Domain.Entity.User;
namespace store.api._.Infrastructure.Controller;

[ApiController]
[Route("api/[Controller]")]
public class PersonController(PersonService personService) : ControllerBase
{
    private readonly PersonService _personService = personService;

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

            ApiResponseBase response = await _personService.GetPerson_serv(request.CodiceFiscale);

            return response switch
            {
                ApiResponse_Success<Person> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPerson_cont()
    {
        try 
        {
            ApiResponseBase response = await _personService.GetAllPerson_serv();

            return response switch
            {
                ApiResponse_Success<IEnumerable<Person>> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [HttpPost]
    public async Task<IActionResult> PostPerson_cont()
    {
        try 
        {
            return Ok();
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [HttpPut]
    public async Task<IActionResult> PutPerson_cont()
    {
        try 
        {
            return Ok();
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePerson_cont()
    {
        try 
        {
            return Ok();
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
}