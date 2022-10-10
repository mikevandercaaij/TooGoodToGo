namespace Core.DomainServices.Repos.Intf
{
    public interface ICanteenEmployeeRepository
    {
        Task<CanteenEmployee> GetCanteenEmployeeByIdAsync(int id);
        Task AddCanteenEmployeeAsync(CanteenEmployee canteenEmployee);
    }
}