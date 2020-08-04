using MGAnonymized.Web.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MGAnonymized.Web.Data.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();

        Movie Get(string Id);

        Task DeleteAsync(string id);

        Task InsertManyAsync(IEnumerable<Movie> entities);

        Task ReplaceAsync(Movie entity);

        Task UpdateAsync(Movie entity);
    }
}