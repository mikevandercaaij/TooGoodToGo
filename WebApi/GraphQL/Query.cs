
namespace WebApi.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<Package>> GetPackages([Service] IPackageService _packageService) => await _packageService.GetAllPackagesAsync();
    }
}