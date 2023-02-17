using Microsoft.AspNetCore.Mvc;
using SegundaPracticaXiangZhou.Models;
using SegundaPracticaXiangZhou.Repositories;

namespace SegundaPracticaXiangZhou.Controllers
{
    public class ComicsController : Controller
    {
        private IRepository repo;

        public ComicsController(IRepository repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Comic> comics = this.repo.GetComics();
            return View(comics);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Comic comics)
        {
            this.repo.InsertComic(comics.Nombre, comics.Imagen,comics.Descripcion);
            return RedirectToAction("Index");
        }
    }
}
