using api.multitracks.com.Models;

namespace api.multitracks.com.Interfaces
{
    public interface IMultitracksProvider
    {
        Task<ICollection<Artist>> SearchAsync(string search);

        Task<ICollection<Song>> ListAsync(PagingParams pagingParams);

        Task<ICollection<Song>> GetAll();

        Task<int> CountSongsAsync();

        Task<bool> AddAsync(Artist course);
    }
}
