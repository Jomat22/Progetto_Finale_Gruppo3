using Microsoft.AspNetCore.Mvc;
using store.api.src.Decorator;
using store.core.src.Domain.Entity.Catalog;
using store.core.src.Interface;

namespace store.api.src.Controller;

public class ProductDecorateRequest
{
    public string Nome { get; set; } = string.Empty;
    public decimal Prezzo { get; set; }
    public List<string> Decoratori { get; set; } = [];
}

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    [HttpPost("decorate")]
    public IActionResult Decorate([FromBody] ProductDecorateRequest request)
    {
        IProduct prodotto = new Product { Nome = request.Nome, Prezzo = request.Prezzo };

        foreach (var dec in request.Decoratori)
        {
            if (dec.ToLower() == "giftwrap")             prodotto = new GiftWrapDecorator(prodotto);
            else if (dec.ToLower() == "expressdelivery") prodotto = new ExpressDeliveryDecorator(prodotto);
            else if (dec.ToLower() == "insurance")       prodotto = new InsuranceDecorator(prodotto);
            else return BadRequest($"Decorator '{dec}' non riconosciuto.");
        }

        return Ok(new
        {
            Descrizione = prodotto.Descrizione(),
            Prezzo      = prodotto.GetPrezzo()
        });
    }
}