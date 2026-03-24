using Microsoft.AspNetCore.Mvc;
using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Infrastructure.Service;
using store.api.src.Dto.Product;
using store.core.src.Domain.Entity.Catalog;
using store.api.src.Decorator;
using store.core.src.Interface;
namespace store.api.src.Controller;

public class ProductDecorateRequest
{
    public string Nome { get; set; } = string.Empty;
    public decimal Prezzo { get; set; }
    public List<string> Decoratori { get; set; } = [];
}

[ApiController]
[Route("api/[Controller]")]
public class ProductController(ProductService serv) : ControllerBase
{
    private readonly ProductService _serv = serv;

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{sku}")]
    public async Task<IActionResult> GetProduct_cont([FromRoute] string sku)
    {
        try
        {
            ProductGetByRequest request = new() { Sku = sku };
            if(sku == "{sku}" || !TryValidateModel(request)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }

            ApiResponseBase response = await _serv.GetProduct_serv(request.Sku);

            return response switch
            {
                ApiResponse_Success<Product> success => Ok(success),
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
    public async Task<IActionResult> GetAllProduct_cont()
    {
        try 
        {
            ApiResponseBase response = await _serv.GetAllProduct_serv();

            return response switch
            {
                ApiResponse_Success<IEnumerable<Product>> success => Ok(success),
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
    public async Task<IActionResult> PostProduct_cont([FromBody] ProductCreateRequest request)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState));
            ApiResponseBase response = await _serv.PostProduct_serv(request);
            
            return response switch
            {
                ApiResponse_Success<Product> success => Ok(success),
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
    public async Task<IActionResult> PutProduct_cont([FromBody] ProductUpdateRequest request)
    {
        try 
        {
            if (!ModelState.IsValid) return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState));
            ApiResponseBase response = await _serv.PutProduct_serv(request);
            
            return response switch
            {
                ApiResponse_Success<Product> success => Ok(success),
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
    [HttpDelete("{sku}")]
    public async Task<IActionResult> DeleteProduct_cont([FromRoute] string sku)
    {
        try
        {
            ProductDeleteRequest request = new() { Sku = sku };
            if(sku == "{sku}" || !TryValidateModel(request)) { return BadRequest(ApiResponseFactory.BadInput_ModelState(ModelState)); }

            ApiResponseBase response = await _serv.DeleteProduct_serv(request.Sku);
            
            return response switch
            {
                ApiResponse_Success<Product> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        } catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }


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