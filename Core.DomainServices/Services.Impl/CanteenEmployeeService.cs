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
        
        public async Task<bool> AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee)
        {
            if (canteenEmployee == null)
                throw new Exception("Deze kantinemedewerker bestaat niet!");
                
            await _canteenEmployeeRepository.AddCanteenEmployeeAsync(canteenEmployee);
            return true;
        }

        public async Task<CanteenEmployee?> GetCanteenEmployeeByIdAsync(string id)
        {
            var canteenEmployee = await _canteenEmployeeRepository.GetCanteenEmployeeByIdAsync(id);

            if (canteenEmployee == null)
                throw new Exception("Deze kantinemedewerker bestaat niet!");

            return canteenEmployee;
        }
    }
}
