using store.api.src.Common;
using store.api.src.Dto.Receipt;

namespace store.api.src.Facade;

public interface IStoreFacade
{
    Task<ApiResponseBase> GetReceiptFacade(int id);
    Task<ApiResponseBase> GetAllReceiptFacade();
    Task<ApiResponseBase> GetReceiptTodayFacade();
    Task<ApiResponseBase> GetReceiptByMetodoFacade(string metodoPagamento);
    Task<ApiResponseBase> CreateReceiptFacade(ReceiptCreateRequest request);
    Task<ApiResponseBase> DeleteReceiptFacade(int id);
    Task<ApiResponseBase> GetStoricoByClientFacade(int clientId);
}