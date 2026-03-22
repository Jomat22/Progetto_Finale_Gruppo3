using Microsoft.AspNetCore.Mvc;
using store.core.src.Interface;
namespace store.api.src.Controller;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentContext _paymentContext;

    public PaymentController(IPaymentContext paymentContext)
    {
        _paymentContext = paymentContext;
    }

    [HttpPost("pay")]
    public IActionResult Pay(string provider, decimal amount)
    {
        var result = _paymentContext.ExecuteStrategy(provider, amount);
        
        if (result.StartsWith("Errore"))
            return BadRequest(result);

        return Ok(result);
    }
}