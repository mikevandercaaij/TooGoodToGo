using Core.DomainServices.Services.Intf;

namespace Core.DomainServices.Services.Impl
{
    public class CanteenEmployeeService : ICanteenEmployeeService
    {
        private readonly ICanteenEmployeeRepository _canteenEmployeeRepository;
        public CanteenEmployeeService(ICanteenEmployeeRepository canteenEmployeeRepository)
        {
            _canteenEmployeeRepository = canteenEmployeeRepository;
        }

        public async Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee)
        {
            await _canteenEmployeeRepository.AddCanteenEmployeeAsync(canteenEmployee);
        }

        public async Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int id) => await _canteenEmployeeRepository.GetCanteenEmployeeByIdAsync(id);
    }
}
