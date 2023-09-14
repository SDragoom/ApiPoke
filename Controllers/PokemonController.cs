using Microsoft.AspNetCore.Mvc;

namespace ApiPoke.Controllers
{
    public class PokemonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
