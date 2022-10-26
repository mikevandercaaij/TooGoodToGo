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
        
        public async Task<Canteen?> GetCanteenByLocationAsync(CanteenLocationEnum location)
        {
            var canteen = await _canteenRepository.GetCanteenByLocationAsync(location);

            if (canteen == null)
                throw new Exception("Deze kantine bestaat niet!");

            return canteen;
        }

        public Task GetCanteenByLocationAsync(object value)
        {
            throw new NotImplementedException();
        }
    }
}
