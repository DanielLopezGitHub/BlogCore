using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly ApplicationDbContext _contexto;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext contexto)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _contexto = contexto;
        }

        // Este primer Action nos retornara una Lista de Todas las Categorias.
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        // - - -- - - - - - -  - - - Metodos Crear
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


        // - - -- - - - - - -  - - - Metodos Editar
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }


        // - - -- - - - - - -  - - - Metodos Editar



        //  - - - - -  Esto es un Endpoint Normal que es llamado desde la DataTable en Javascript.
        #region Llamadas a la Api
        [HttpGet]
        public IActionResult GetAll() 
        {
            // Opcion 1 en Procedimiento Almacenado
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Categoria.Get(id);
            if(objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar la categoria" });
            }

            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Categoria borrada correctamente" });
        }
#endregion
    }
}
