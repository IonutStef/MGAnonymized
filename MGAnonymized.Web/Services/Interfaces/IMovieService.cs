using MGAnonymized.Web.Common.Models;
using System.Collections.Generic;

namespace MGAnonymized.Web.Services.Interfaces
{
    public interface IMovieService
    {
        ICollection<Movie> GetMovies();
    }
}