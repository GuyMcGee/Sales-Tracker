
public interface IProductService
{
    Task<bool> CreateProductAsync(ProductCreate itemCreate);
    Task<IEnumerable<ProductListItem>> GetAllProductsAsync();
    Task<ProductDetails> GetProductByIdAsync(int productId);
    Task<bool> EditProductAsync(ProductEdit request);
    Task<bool> DeleteProductAsync(int productId);
}
