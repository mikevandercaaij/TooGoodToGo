namespace Core.DomainServices
{
    public interface ICanteenRepository
    {
        Task<IEnumerable<Canteen>> GetAllCanteensAsync();
        Task<Canteen> GetCanteenByIdAsync(int id);

    }
}