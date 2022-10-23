
namespace WebApi.GraphQL
{
    public class Query
    {
        public async Task<IEnumerable<Package>> GetPackages([Service] PackageRepository packageRepository) => await packageRepository.GetAllPackagesAsync();
    }
}