namespace Core.DomainServices.Repos.Intf
{
    public interface ICanteenRepository
    {
        Task<IEnumerable<Canteen>> GetAllCanteensAsync();
        Task<Canteen?> GetCanteenByIdAsync(int id);

    }
}