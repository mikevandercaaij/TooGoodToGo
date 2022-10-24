using Microsoft.AspNetCore.Mvc.Rendering;

namespace Portal.Models
{
    public class ViewModelHelper
    {
        public static PackageDetailsViewModel GetPackageDetailsViewModel(Package package, CanteenLocationEnum canteenLocation)
        {
            var model = new PackageDetailsViewModel
            {
                Package = package,
            };

            if (package?.ReservedBy != null)
                model.Name = package.ReservedBy.FirstName + " " + package.ReservedBy.LastName;

            model.CanteenEmployeeLocation = canteenLocation;

            return model;
        }
        public static PackageModel GetPackageViewModel(Package package, IList<SelectListItem>? availableProducts)
        {
            var module = new PackageModel()
            {
                PackageId = package!.PackageId,
                Name = package!.Name,
                Products = package!.Products,
                PickUpTime = package!.PickUpTime,
                LatestPickUpTime = package!.LatestPickUpTime,
                Price = package!.Price,
                MealType = package!.MealType,
                AvailableProducts = availableProducts,
            };

            var _selectedProducts = new List<string>();

            foreach (Product p in package.Products)
            {
                _selectedProducts.Add(p.Name!);
            }

            module.SelectedProducts = _selectedProducts;

            return module;
        }
    }
}
