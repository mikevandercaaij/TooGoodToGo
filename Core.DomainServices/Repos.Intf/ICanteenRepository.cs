using Core.Domain.Enums;

namespace Core.DomainServices.Repos.Intf
{
    public interface ICanteenRepository
    {
        Task<IEnumerable<Canteen>> GetAllCanteensAsync();
        Task<Canteen?> GetCanteenByLocationAsync(CanteenLocationEnum id);

    }
}