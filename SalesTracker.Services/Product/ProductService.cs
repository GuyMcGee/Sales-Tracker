using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductAsync(ProductCreate model)
        {
            var entity = new ProductEntity
            {
                Name = model.Name,
                Description = model.Description,
                Cost = model.Cost,
                ProductTypeId = model.ProductTypeId
                
            };
            
            _context.Products.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<IEnumerable<ProductListItem>> GetAllProductsAsync()
        {
            return await _context.Products.Select(i => new ProductListItem
            {
                Id = i.Id,
                Name = i.Name,
                Cost = i.Cost
            }).ToListAsync();
        }

        public async Task<ProductDetails> GetProductByIdAsync(int productId)
        {
            ProductEntity product = await _context.Products.Include(i => i.ProductType).FirstOrDefaultAsync(i => i.Id == productId);
            if (product is null)
            {
                return null;
            }
            // manuly mapping a GameEntity Object to a GameDetails Object
            return new ProductDetails
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Cost = product.Cost,
                ProductType = new ProductTypeListItem
                {
                    Id = product.ProductType.Id,
                    Name = product.ProductType.Name
                }
            };
        }

        public async Task<bool> EditProductAsync(ProductEdit request)
        {
            var productEntity = await _context.Products.FindAsync(request.Id);
            
            if (productEntity == null)
                return false;

            productEntity.Name = request.Name;
            productEntity.Description = request.Description;
            productEntity.Cost = request.Cost;
            productEntity.ProductTypeId = request.ProductTypeId;

            var numberOfChanges = await _context.SaveChangesAsync();

            return numberOfChanges == 1;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            // Find the note by the given Id
            var productEntity = await _context.Products.FindAsync(productId);

            // Validate the note exists and is owned by the user
            if (productEntity == null)
                return false;

            // Remove the note from the DbContext and assert that the one change was saved
            _context.Products.Remove(productEntity);
            return await _context.SaveChangesAsync() == 1;

        }
    }