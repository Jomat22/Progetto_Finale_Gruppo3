using AutoMapper;
using store.api.src.Common;
using store.api.src.Decorator;
using store.api.src.Dto.Receipt;
using store.api.src.Factory;
using store.api.src.Helper;
using store.api.src.Infrastructure.Repo;
using store.core.src.Domain.Entity.Sales;
using store.core.src.Interface;
namespace store.api.src.Infrastructure.Service;

public class ReceiptService(ReceiptRepository repo, ProductRepository repoProduct, IPaymentContext paymentContext)
{
    private readonly ReceiptRepository _repo = repo;
    private readonly ProductRepository _repoProduct = repoProduct;
    private readonly IPaymentContext _paymentContext = paymentContext;

    public async Task<ApiResponseBase> GetReceipt_serv(int id)
    {
        try
        {
            Receipt? receipt = await _repo.GetReceiptById_repo(id);
            if (receipt is null) { return ApiResponseFactory.NotFound(); }

            return ApiResponseFactory.Success(receipt);
        }
        catch (Exception) { throw; }
    }

    public async Task<ApiResponseBase> GetAllReceipt_serv()
    {
        try
        {
            IEnumerable<Receipt> receipts = await _repo.GetAllReceipt_repo();
            if (!receipts.Any()) { return ApiResponseFactory.SuccessNoContent(receipts); }

            return ApiResponseFactory.Success(receipts);
        }
        catch (Exception) { throw; }
    }

    public async Task<ApiResponseBase> GetReceiptByMetodo_serv(string metodoPagamento)
    {
        try
        {
            IEnumerable<Receipt> receipts = await _repo.GetReceiptByMetodo_repo(metodoPagamento);
            if (!receipts.Any()) { return ApiResponseFactory.SuccessNoContent(receipts); }

            return ApiResponseFactory.Success(receipts);
        }
        catch (Exception) { throw; }
    }

    public async Task<ApiResponseBase> GetReceiptToday_serv()
    {
        try
        {
            IEnumerable<Receipt> receipts = await _repo.GetReceiptToday_repo();
            decimal totaleGiornaliero = receipts.Sum(r => r.TotaleDefinitivo);

            return ApiResponseFactory.Success(new
            {
                TotaleGiorno = totaleGiornaliero,
                NumeroScontrini = receipts.Count(),
                Scontrini = receipts
            });
        }
        catch (Exception) { throw; }
    }

    public async Task<ApiResponseBase> PostReceipt_serv(ReceiptCreateRequest request)
    {
        try
        {
            var dettagli = new List<ReceiptDetail>();
            decimal totale = 0;

            foreach (var p in request.Prodotti)
            {
                var prodotto = await _repoProduct.GetProductById_repo(p.ProdottoId);
                if (prodotto is null) { return ApiResponseFactory.NotFound(); }

                if (prodotto.Quantita < p.Quantita)
                {
                    return new ApiResponse_Error
                    {
                        IsSuccess = false,
                        StatusCode = 422,
                        Message = $"Operazione fallita per stock insufficiente di {prodotto}. Disponibili {prodotto.Quantita}.",
                        Error = []
                    };
                }

                //decorator
                IProduct productDec = prodotto;
                if (p.GiftWrap)
                {
                    productDec = new GiftWrapDecorator(productDec);
                }

                if (p.Express)
                {
                    productDec = new ExpressDeliveryDecorator(productDec);
                }

                if (p.Assicurazione)
                {
                    productDec = new InsuranceDecorator(productDec);
                }

                decimal prezzoAdd = productDec.GetPrezzo() * p.Quantita;
                totale += prezzoAdd;

                dettagli.Add(new ReceiptDetail
                {
                    ProdottoId = prodotto.Id,
                    Quantita = p.Quantita,
                    PrezzoTotale = prezzoAdd,
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = DateTime.UtcNow
                });

                prodotto.Quantita -= p.Quantita;
                DataHelper.InputDataAddTimestamp(prodotto);
            }

            //strategy
            string pagamento = _paymentContext.ExecuteStrategy(request.MetodoPagamento, totale);

            var receipt = new Receipt
            {
                ClientId = request.ClientId,
                DataEmissione = DateTime.UtcNow,
                MetodoPagamento = request.MetodoPagamento,
                TotaleDefinitivo = totale,
                RicevutaDettagli = dettagli,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
            };

            int numRows = await _repo.PostReceipt_repo(receipt);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(receipt); }

            return ApiResponseFactory.Success(receipt);
        }
        catch (Exception) { throw; }
    }

    public async Task<ApiResponseBase> DeleteReceipt_serv(int id)
    {
        try
        {
            Receipt? receipt = await _repo.GetReceiptById_repo(id, false);
            if (receipt is null) { return ApiResponseFactory.NotFound(); }

            foreach (var d in receipt.RicevutaDettagli)
            {
                var prodotto = await _repoProduct.GetProductById_repo(d.ProdottoId, asNoTracking: false);
                if (prodotto is not null)
                {
                    prodotto.Quantita += d.Quantita;
                    DataHelper.InputDataUpdateTimestamp(prodotto);
                }
            }

            int numRows = await _repo.DeleteReceipt_repo(receipt);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(receipt); }

            return ApiResponseFactory.Success(receipt);
        }
        catch (Exception) { throw; }
    }

    public async Task<ApiResponseBase> GetStoricoByClient_serv(int clientId)
    {
        try
        {
            IEnumerable<Receipt> receipts = await _repo.GetReceiptByClient_repo(clientId);

            return ApiResponseFactory.Success(new
            {
                ClientId = clientId,
                TotaleSpeso = receipts.Sum(r => r.TotaleDefinitivo),
                NumeroOrdini = receipts.Count(),
                Scontrini = receipts
            });
        }
        catch (Exception) { throw; }
    }
}