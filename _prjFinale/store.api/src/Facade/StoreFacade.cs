using store.api.src.Common;
using store.api.src.Dto.Receipt;
using store.api.src.Infrastructure.Service;

namespace store.api.src.Facade;

public class StoreFacade(ReceiptService service) : IStoreFacade
{
    private readonly ReceiptService _service = service;

    public async Task<ApiResponseBase> GetReceiptFacade(int id)
    {
        return await _service.GetReceipt_serv(id);
    }
    public async Task<ApiResponseBase> GetAllReceiptFacade()
    {
        return await _service.GetAllReceipt_serv();
    }
    public async Task<ApiResponseBase> GetReceiptTodayFacade()
    {
        return await _service.GetReceiptToday_serv();
    }
    public async Task<ApiResponseBase> GetReceiptByMetodoFacade(string metodoPagamento)
    {
        return await _service.GetReceiptByMetodo_serv(metodoPagamento);
    }
    public async Task<ApiResponseBase> CreateReceiptFacade(ReceiptCreateRequest req)
    {
        return await _service.PostReceipt_serv(req);
    }
    public async Task<ApiResponseBase> DeleteReceiptFacade(int id)
    {
        return await _service.DeleteReceipt_serv(id);
    }

    public async Task<ApiResponseBase> GetStoricoByClientFacade(int clientId)
    {
        return await _service.GetStoricoByClient_serv(clientId);
    }
}