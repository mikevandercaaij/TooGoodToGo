using Core.Domain.Enums;
using Core.DomainServices.Services.Intf;

namespace Core.DomainServices.Services.Impl
{
    public class CanteenService : ICanteenService
    {
        private readonly ICanteenRepository _canteenRepository;
        
        public CanteenService(ICanteenRepository canteenRepository)
        {
            _canteenRepository = canteenRepository;
        }
        
        public Task<IEnumerable<Canteen>> GetAllCanteensAsync() => _canteenRepository.GetAllCanteensAsync();
        public Task<Canteen?> GetCanteenByLocationAsync(CanteenLocationEnum location) => _canteenRepository.GetCanteenByLocationAsync(location);
    }
}
