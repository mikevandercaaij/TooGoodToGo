using Microsoft.AspNetCore.Mvc.Rendering;

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
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            
            if (product == null)
                throw new Exception("Dit product bestaat niet!");

            return product;
        }
        public async Task<Product?> GetProductByNameAsync(string name)
        {
            var product = await _productRepository.GetProductByNameAsync(name);

            if (product == null)
                throw new Exception("Dit product bestaat niet!");

            return product;
        }
        public async Task<IList<SelectListItem>> GetAllSelectListItems()
        {
            List<SelectListItem> productList = new();
            var products = await _productRepository.GetAllProductsAsync();

            foreach (var product in products)
            {
                productList.Add(new SelectListItem { Text = product.Name, Value = product.Name });
            }
            
            return productList.OrderBy(p => p.Text).ToList();
        }
    }
}
