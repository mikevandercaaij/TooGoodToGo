using Core.Domain.Enums;

namespace Core.DomainServices.Repos.Intf
{
    public interface ICanteenRepository
    {
        Task<Canteen?> GetCanteenByLocationAsync(CanteenLocationEnum location);
    }
}