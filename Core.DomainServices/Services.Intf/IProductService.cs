using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.DomainServices.Services.Intf
    
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductByNameAsync(string name);
        Task<IList<SelectListItem>> GetAllSelectListItems();
    }
}
