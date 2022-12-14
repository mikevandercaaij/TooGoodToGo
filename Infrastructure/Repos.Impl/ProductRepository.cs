namespace Infrastructure.Repos.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetProductByIdAsync(int id) => await _context.Products.FindAsync(id);
        public async Task<Product?> GetProductByNameAsync(string name) => await _context.Products.Where(p => p.Name == name).FirstOrDefaultAsync();
        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _context.Products.ToListAsync();
    }
}
