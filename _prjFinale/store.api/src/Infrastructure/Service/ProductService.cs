using AutoMapper;
using store.api.src.Common;
using store.api.src.Factory;
using store.api.src.Dto.Product;
using store.api.src.Helper;
using store.api.src.Infrastructure.Repo;
using store.core.src.Domain.Entity.Catalog;
namespace store.api.src.Infrastructure.Service;

public class ProductService(ProductRepository repo, IMapper mapper)
{
    private readonly ProductRepository _repo = repo;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponseBase> GetProduct_serv(string sku) {
        try
        {
            Product? product = await _repo.GetProductBySku_repo(sku);
            if (product is null) { return ApiResponseFactory.NotFound(); }

            return ApiResponseFactory.Success(product);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> GetAllProduct_serv() {
        try
        {
            IEnumerable<Product> people = await _repo.GetAllProduct_repo();
            if (!people.Any()) { return ApiResponseFactory.SuccessNoContent(people); }
            
            return ApiResponseFactory.Success(people); 
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PostProduct_serv(ProductCreateRequest request) {
        try
        {
            Product? existingProduct = await _repo.GetProductBySku_repo(request.Sku);
            if (existingProduct is not null) { return ApiResponseFactory.Conflict(); }

            Product product = _mapper.Map<Product>(request);
            DataHelper.InputDataFormatUppercase(product);
            DataHelper.InputDataAddTimestamp(product);

            int numRows = await _repo.PostProduct_repo(product);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(product); }

            return ApiResponseFactory.Success(product);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> PutProduct_serv(ProductUpdateRequest request) {
        try
        {
            Product? existingProduct = await _repo.GetProductBySku_repo(request.Sku, false);
            if (existingProduct is null) { return ApiResponseFactory.NotFound(); }

            _mapper.Map(request, existingProduct);
            DataHelper.InputDataFormatUppercase(existingProduct);
            DataHelper.InputDataUpdateTimestamp(existingProduct);
            
            int numRows = await _repo.PutProduct_repo(); // Non serve passarlo perchè ha già tracciato i cambiamenti i nmemoria. Chiamo solo il 'SaveChanges'
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(existingProduct); }

            return ApiResponseFactory.Success(existingProduct);
        } catch (Exception) { throw; }
    }
    
    public async Task<ApiResponseBase> DeleteProduct_serv(string sku) {
        try
        {
            Product? existingProduct = await _repo.GetProductBySku_repo(sku, false);
            if (existingProduct is null) { return ApiResponseFactory.NotFound(); }

            int numRows = await _repo.DeleteProduct_repo(existingProduct);
            if (numRows is 0) { return ApiResponseFactory.SuccessNoChanges(existingProduct); }

            return ApiResponseFactory.Success(existingProduct);
        } catch (Exception) { throw; }
    }
} 