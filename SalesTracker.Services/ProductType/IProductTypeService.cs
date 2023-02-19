
public interface IProductTypeService
{
    Task<bool> CreateProductTypeAsync(ProductTypeCreate productTypeCreate);
    Task<IEnumerable<ProductTypeListItem>> GetAllProductTypeAsync();
    Task<ProductTypeDetails> GetProductTypeByIdAsync(int itemId);
    Task<bool> EditProductTypeAsync(ProductTypeEdit request);
    Task<bool> DeleteProductTypeAsync(int itemId);
}