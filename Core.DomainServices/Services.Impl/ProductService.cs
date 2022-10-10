namespace Core.DomainServices.Services.Impl
{
    public class ProductService : IProductService
    { 
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _productRepository.GetAllProductsAsync();

        public async Task<Product> GetProductByIdAsync(int id) => await _productRepository.GetProductByIdAsync(id);
    }

}
