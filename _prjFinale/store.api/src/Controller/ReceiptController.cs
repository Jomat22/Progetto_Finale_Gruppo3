using Microsoft.AspNetCore.Mvc;
using store.api.src.Common;
using store.api.src.Dto.Receipt;
using store.api.src.Factory;
using store.api.src.Infrastructure.Service;
using store.core.src.Domain.Entity.Sales;

namespace store.api.src.Controller;

[ApiController]
[Route("api/[controller]")]
public class ReceiptController(ReceiptService service) : ControllerBase
{
    private readonly ReceiptService _service = service;

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetReceipt_cont([FromRoute] int id)
    {
        try
        {
            ReceiptGetByRequest req = new() { Id = id };

            ApiResponseBase response = await _service.GetReceipt_serv(req.Id);

            return response switch
            {
                ApiResponse_Success<Receipt> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        }
        catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetAllReceipt_cont()
    {
        try
        {
            ApiResponseBase response = await _service.GetAllReceipt_serv();

            return response switch
            {
                ApiResponse_Success<IEnumerable<Receipt>> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        }
        catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("oggi")]
    public async Task<IActionResult> GetReceiptToday_cont()
    {
        try
        {
            ApiResponseBase response = await _service.GetReceiptToday_serv();

            return response switch
            {
                ApiResponse_Success<IEnumerable<Receipt>> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        }
        catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("metodopagamento/{metodoPagamento}")]
    public async Task<IActionResult> GetReceiptByMetodo_cont([FromRoute] string metodoPagamento)
    {
        try
        {
            ApiResponseBase response = await _service.GetReceiptByMetodo_serv(metodoPagamento);

            return response switch
            {
                ApiResponse_Success<IEnumerable<Receipt>> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        }
        catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<IActionResult> PostReceipt_cont([FromBody] ReceiptCreateRequest request)
    {
        try
        {
            ApiResponseBase response = await _service.PostReceipt_serv(request);

            return response switch
            {
                ApiResponse_Success<Receipt> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        }
        catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }

[ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteReceipt_cont([FromRoute] int id)
    {
        try
        {
            ReceiptGetByRequest req = new() { Id = id };

            ApiResponseBase response = await _service.DeleteReceipt_serv(req.Id);

            return response switch
            {
                ApiResponse_Success<Receipt> success => Ok(success),
                ApiResponse_Error error => StatusCode(error.StatusCode, error.Message),
                _ => StatusCode(500, ApiResponseFactory.InternalServerError()),
            };
        }
        catch (Exception ex) { return StatusCode(500, $"Dettaglio dell'eccezione -> {ex.Message}"); }
    }

}