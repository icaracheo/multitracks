using api.multitracks.com.Interfaces;
using api.multitracks.com.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace api.multitracks.com.Providers
{
    public class SqlServerMultitracksProvider : IMultitracksProvider
    {
        private readonly MultiTracksDBContext db;

        public SqlServerMultitracksProvider(MultiTracksDBContext dBContext)
        {
            this.db = dBContext;
        }

        public async Task<bool> AddAsync(Artist artist)
        {
            artist.DateCreation = DateTime.Now;
            db.Artists.Add(artist);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Song>> ListAsync(PagingParams pagingParams)
        {
            if (pagingParams.Order.ToLower() != "asc")
            {
                return await db.Songs
                .OrderByDescending(x => x.SongId)
                .Skip(pagingParams.Items * (pagingParams.Page - 1))
                .Take(pagingParams.Items)
                .ToListAsync();
            }
            else
            {
                return await db.Songs
                .OrderBy(x => x.SongId)
                .Skip(pagingParams.Items * (pagingParams.Page - 1))
                .Take(pagingParams.Items)
                .ToListAsync();
            }           
        }

        public async Task<ICollection<Song>> GetAll()
        {
            return await db.Songs.ToListAsync();
        }

        public async Task<int> CountSongsAsync() => db.Songs.Count();

        public async Task<ICollection<Artist>> SearchAsync(string name)
        {
            var raw = db.Artists.FromSqlRaw($"SELECT * FROM Artist WHERE title LIKE '%{name}%'");
            var results = await raw.ToListAsync();

            return results;
        }
    }
}
