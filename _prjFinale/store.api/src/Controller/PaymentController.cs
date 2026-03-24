using Microsoft.AspNetCore.Mvc;
using store.api.src.Factory;
using store.core.src.Interface;

namespace store.api.src.Controller;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(IPaymentContext paymentContext) : ControllerBase
{
    private readonly IPaymentContext _paymentContext = paymentContext;

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public IActionResult PostPayment_cont([FromQuery] string provider, [FromQuery] decimal amount)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(provider)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }
            if (amount <= 0) { return BadRequest("L'importo deve essere maggiore di 0."); }

            string risultato = _paymentContext.ExecuteStrategy(provider, amount);

            if (risultato.StartsWith("Errore"))
                return BadRequest(new { IsSuccess = false, Message = risultato });

            return Ok(new { IsSuccess = true, Message = risultato });
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }
}