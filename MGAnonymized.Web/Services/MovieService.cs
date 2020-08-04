using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MGAnonymized.Web.Common.Models;
using MGAnonymized.Web.Data.Repositories;
using MGAnonymized.Web.Infrastructure;
using MGAnonymized.Web.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MGAnonymized.Web.Services
{
    public class MovieService : IMovieService
    {
        private readonly ILogger<MovieService> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IDownloadService _downloadService;
        private readonly MGAnonymizedOptions _options;

        public MovieService(ILogger<MovieService>  logger, IOptions<MGAnonymizedOptions> options, IMovieRepository movieRepository, IDownloadService downloadService)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _downloadService = downloadService;
            _options = options.Value;
        }

        public ICollection<Movie> GetMovies()
        {
            var movies = _movieRepository.GetAll();

            if(movies == null || movies.Count() == 0)
            {
                _logger.LogWarning("No movie could be retrieved.");
            }
            
            return movies.ToList();
        }
    }
}