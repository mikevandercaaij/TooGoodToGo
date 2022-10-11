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
        public Task<Canteen?> GetCanteenByIdAsync(int id) => _canteenRepository.GetCanteenByIdAsync(id);
    }
}
