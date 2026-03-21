using Microsoft.AspNetCore.Mvc;
using store.api._.Common;
using store.api._.Infrastructure.Service;
using store.core._.Domain.Entity.User;
namespace store.api._.Infrastructure.Controller;

[ApiController]
[Route("api/[Controller]")]
public class PersonController(PersonService personService) : ControllerBase
{
    private readonly PersonService personService = personService;

    [HttpGet]
    public async Task<IActionResult> GetPerson_cont()
    {
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPerson_cont()
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> PostPerson_cont()
    {
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> PutPerson_cont()
    {
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePerson_cont()
    {
        return Ok();
    }
}