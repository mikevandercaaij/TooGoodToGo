namespace Core.DomainServices.Services.Intf
{
    public interface ICanteenEmployeeService
    {
        Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int id);
        Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee);
    }
}
