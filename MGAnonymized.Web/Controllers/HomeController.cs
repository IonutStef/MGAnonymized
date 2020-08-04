using Microsoft.AspNetCore.Mvc;
using MGAnonymized.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace MGAnonymized.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;
        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            var movies = _movieService.GetMovies();

            return View(movies);
        }
    }
}