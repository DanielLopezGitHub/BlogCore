using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
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

        #region Llamadas a la Api
        [HttpGet]
        public IActionResult GetAll() 
        {
            // Opcion 1 en Procedimiento Almacenado
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }
        #endregion
    }
}
