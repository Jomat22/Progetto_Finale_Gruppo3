using Microsoft.AspNetCore.Mvc;
using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Infrastructure.Service;
namespace store.api.src.Controller;

[ApiController]
[Route("api/[controller]")]
class PaymentController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPaymentController()
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> PostPaymentController()
    {
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> PutPaymentController()
    {
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePaymentController()
    {
        return Ok();
    }
}