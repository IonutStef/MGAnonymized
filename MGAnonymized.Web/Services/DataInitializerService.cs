using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MGAnonymized.Web.Common.Models;
using MGAnonymized.Web.Data.Repositories;
using MGAnonymized.Web.Infrastructure;
using MGAnonymized.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MGAnonymized.Web.Services
{
    public class DataInitializerService : IDataInitializerService
    {
        private readonly ILogger<DataInitializerService> _logger;
        private readonly IDownloadService _downloadService;
        private readonly IMovieRepository _movieRepository;
        private readonly MGAnonymizedOptions _options;

        public DataInitializerService(ILogger<DataInitializerService> logger, IOptions<MGAnonymizedOptions> options, IDownloadService downloadService, IMovieRepository movieRepository)
        {
            _logger = logger;
            _downloadService = downloadService;
            _movieRepository = movieRepository;
            _options = options.Value;
        }

        public async Task InitializeData()
        {
            var serverMovies = await _downloadService.ReadJsonToObject<ICollection<Movie>>(_options.MoviesSourceUrl);
            var storedMovies = _movieRepository.GetAll();

            if(storedMovies == null || storedMovies.Count() == 0)
            {
                _logger.LogInformation("No movies were found in the storage.");

                await SaveMovieImagesLocally(serverMovies);

                await _movieRepository.InsertManyAsync(serverMovies);

                return;
            }

            var updatedMovies = from svm in serverMovies
                                join stm in storedMovies on svm.Id equals stm.Id
                                where svm.LastUpdated != stm.LastUpdated
                                select svm;

            _logger.LogWarning($"Movies to update: {string.Join(", ", updatedMovies.Select(m => m.Id))}");

            foreach(var movie in updatedMovies)
            {
                await SaveMovieImagesLocally(movie);

                await _movieRepository.ReplaceAsync(movie);
            }
        }

        private async Task SaveMovieImagesLocally(IEnumerable<Movie> movies)
        {
            foreach (var movie in movies)
            {
                await SaveMovieImagesLocally(movie);
            }
        }

        private async Task SaveMovieImagesLocally(Movie movie)
        {
            if (!movie.ImagesSavedLocally)
            {
                _logger.LogWarning($"Movie with Id: {movie.Id} does not have images saved locally.");

                var movieStaticFilesDirectoryName = $"images/Movies" +
                        $"/{movie.Id}";

                if (Directory.Exists($"wwwroot/{movieStaticFilesDirectoryName}"))
                {
                    Directory.Delete($"wwwroot/{movieStaticFilesDirectoryName}", true);
                }

                Directory.CreateDirectory($"wwwroot/{movieStaticFilesDirectoryName}");
                Directory.CreateDirectory($"wwwroot/{movieStaticFilesDirectoryName}/" +
                        $"{nameof(movie.CardImages)}");
                Directory.CreateDirectory($"wwwroot/{movieStaticFilesDirectoryName}/" +
                        $"{nameof(movie.KeyArtImages)}");

                foreach (var cardImage in movie.CardImages)
                {
                    var localImagePath = $"{movieStaticFilesDirectoryName}/" +
                        $"{nameof(movie.CardImages)}/" +
                        $"{cardImage.ShortName}";

                    await _downloadService.SaveFile(cardImage.Url, $"wwwroot/{localImagePath}");

                    cardImage.LocalPath = localImagePath;
                }

                foreach (var keyArtImage in movie.KeyArtImages)
                {
                    var localImagePath = $"{movieStaticFilesDirectoryName}/" +
                        $"{nameof(movie.KeyArtImages)}/" +
                        $"{keyArtImage.ShortName}";

                    await _downloadService.SaveFile(keyArtImage.Url, $"wwwroot/{localImagePath}");

                    keyArtImage.LocalPath = localImagePath;
                }

                movie.ImagesSavedLocally = true;
            }
        }
    }
}