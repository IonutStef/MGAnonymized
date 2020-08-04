using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JsonFlatFileDataStore;
using Microsoft.Extensions.Options;
using MGAnonymized.Web.Common.Models;
using MGAnonymized.Web.Infrastructure;

namespace MGAnonymized.Web.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    { 
        private readonly MGAnonymizedOptions _options;
        public MovieRepository(IOptions<MGAnonymizedOptions> options)
        {
            _options = options.Value;
        }

        public async Task DeleteAsync(string id)
        {
            using (var store = new DataStore(_options.LocalStorageFilePath))
            {
                var movieCollection = store.GetCollection<Movie>();

                await movieCollection.DeleteOneAsync(m => m.Id == id);
            }
        }

        public Movie Get(string id)
        {
            using (var store = new DataStore(_options.LocalStorageFilePath))
            {
                var movieCollection = store.GetCollection<Movie>();

                var movie = movieCollection.AsQueryable()
                    .FirstOrDefault(m => m.Id == id);

                return movie;
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            using (var store = new DataStore(_options.LocalStorageFilePath))
            {
                var movieCollection = store.GetCollection<Movie>();

                var movies = movieCollection.AsQueryable();
                return movies;
            }
        }

        public async Task InsertManyAsync(IEnumerable<Movie> entities)
        {
            using (var store = new DataStore(_options.LocalStorageFilePath))
            {
                var movieCollection = store.GetCollection<Movie>();

                await movieCollection.InsertManyAsync(entities);
            }
        }

        public async Task ReplaceAsync(Movie entity)
        {
            using (var store = new DataStore(_options.LocalStorageFilePath))
            {
                var movieCollection = store.GetCollection<Movie>();

                await movieCollection.ReplaceOneAsync(m => m.Id == entity.Id, entity, true);
            }
        }

        public async Task UpdateAsync(Movie entity)
        {
            using (var store = new DataStore(_options.LocalStorageFilePath))
            {
                var movieCollection = store.GetCollection<Movie>();

                await movieCollection.UpdateOneAsync(m => m.Id == entity.Id, entity);
            }
        }
    }
}