using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Models;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        // - - - --  - - - - - - Inyecciones de Dependencias - - - - - - - - -
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostEnvironment = hostEnvironment;
        }
        // - - - --  - - - - - - Inyecciones de Dependencias - - - - - - - - -

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // - - ---  - - - - - - - - Metodos CREAR - - - - - - 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (HttpContext.Request.Form.Files.Any() && !String.IsNullOrEmpty(slider.Nombre))
            {
                string rutaPrincipal = _hostEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                
                
                // Nuevo slider
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;
                slider.Estado = true;

                _contenedorTrabajo.Slider.Add(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
                
            }
            
            return View(slider);
        }


        // - - ---  - - - - - - - - Metodos EDITAR - - - - - - 
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sliderFromDb = _contenedorTrabajo.Slider.Get(id);

            if (sliderFromDb == null)
            {
                return NotFound();
            }

            return View(sliderFromDb);
        }



        // - - - - - - - - - Metodos OBTENER TODOS LOS ARTICULOS y ELIMINAR - - - - - - -
        #region Llamadas a la Api

        [HttpGet]
        public IActionResult GetAll()
        {
            // Opcion 1 en Procedimiento Almacenado
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }

        public IActionResult Delete(int id)
        {
            var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(id);
            string rutaDirectorioPrincipal = _hostEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaImagen)) // Borrar la Imagen del Articulo
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (articuloDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando Articulo" });
            }

            _contenedorTrabajo.Articulo.Remove(articuloDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Articulo borrada correctamente" });
        }

        #endregion
    }
}
