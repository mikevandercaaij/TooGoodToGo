namespace Core.DomainServices.Repos.Intf
{
    public interface ICanteenEmployeeRepository
    {
        Task<CanteenEmployee?> GetCanteenEmployeeByIdAsync(string id);
        Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee);
    }
}