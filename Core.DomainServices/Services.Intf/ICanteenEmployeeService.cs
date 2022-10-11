namespace Core.DomainServices.Services.Intf
{
    public interface ICanteenEmployeeService
    {
        Task<CanteenEmployee?> GetCanteenEmployeeByIdAsync(string id);
        Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee);
    }
}
