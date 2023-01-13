
using Microsoft.EntityFrameworkCore;

public class ProductTypeService : IProductTypeService
{
    private readonly AppDbContext _context;
        public ProductTypeService(AppDbContext context)
        {
            _context = context;
        }
    public async Task<bool> CreateProductTypeAsync(ProductTypeCreate model)
    {
        var entity = new ProductTypeEntity
            {
                Name = model.Name
            };

            _context.ProductType.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
    }

    public async Task<IEnumerable<ProductTypeListItem>> GetAllProductTypeAsync()
        {
            return await _context.ProductType.Select(i => new ProductTypeListItem
            {
                Id = i.Id,
                Name = i.Name
            }).ToListAsync();
        }

        public async Task<ProductTypeDetails> GetProductTypeByIdAsync(int productTypeId)
        {
            var product = await _context.ProductType.Include(p => p.Items).FirstOrDefaultAsync(p => p.Id == productTypeId);
            if (product is null)
            {
                return null;
            }
            // manuly mapping a GameEntity Object to a GameDetails Object
            return new ProductTypeDetails
            {
                Id = product.Id,
                Name = product.Name,
                Items = product.Items.Select(i => new ItemListItem
                {
                    Id = i.Id,
                    Name = i.Name
                }).ToList()
            };
        }

        public async Task<bool> EditProductTypeAsync(ProductTypeEdit request)
        {
            var productTypeEntity = await _context.ProductType.FindAsync(request.Id);
            
            if (productTypeEntity == null)
                return false;

            productTypeEntity.Name = request.Name;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteProductTypeAsync(int productTypeId)
        {
            // Find the note by the given Id
            var productTypeEntity = await _context.ProductType.FindAsync(productTypeId);

            // Validate the note exists and is owned by the user
            if (productTypeEntity == null)
                return false;

            // Remove the note from the DbContext and assert that the one change was saved
            _context.ProductType.Remove(productTypeEntity);
            return await _context.SaveChangesAsync() == 1;
        }
}