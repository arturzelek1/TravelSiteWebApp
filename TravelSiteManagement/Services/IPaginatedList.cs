namespace TravelSiteWeb.Services
{
    public interface IPaginatedList<T>
    {
        Task<IPaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize);
    }

}
