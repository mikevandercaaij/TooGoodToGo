using Core.DomainServices.Services.Intf;

namespace Core.DomainServices.Services.Impl
{
    public class CanteenService : ICanteenService
    {
        public Task<IEnumerable<Canteen>> GetAllCanteensAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Canteen> GetCanteenByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
