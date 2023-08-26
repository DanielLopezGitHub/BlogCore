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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            Console.WriteLine(slider.Nombre);
            var objFromDb = _contenedorTrabajo.Slider.Get(slider.Id);
            string rutaPrincipal = _hostEnvironment.WebRootPath; // Obteniendo ruta principal de la Aplicacion (C:\Users\...\...\BlogCore\wwwroot)

            if (HttpContext.Request.Form.Files.Any()) // Si viene una nueva imagen
            {
                string nombreArchivo = Guid.NewGuid().ToString(); // Creando un nombre unico para la imagen nueva
                var rutaCompletaSlidersSubidos = Path.Combine(rutaPrincipal, @"imagenes\sliders"); // Preparando la ruta final donde se suben los sliders
                var extension = Path.GetExtension(HttpContext.Request.Form.Files[0].FileName); // Obteniendo la Extencion del slider subido
                var rutaSliderExistente = Path.Combine(rutaPrincipal, objFromDb.UrlImagen.TrimStart('\\')); // Obteniendo ruta completa de slider existente

                if (System.IO.File.Exists(rutaSliderExistente)) // Si ya existe un slider asociado a este objeto, se eliminara
                {
                    System.IO.File.Delete(rutaSliderExistente);
                }

                // Subimos el nuevo slider que viene en los Http Files
                using (var fileStreams = new FileStream(Path.Combine(rutaCompletaSlidersSubidos, nombreArchivo + extension), FileMode.Create))
                {
                    HttpContext.Request.Form.Files[0].CopyTo(fileStreams); // Aqui compiamos ya el Slider subido a la ruta de "fileStreams"
                }

                slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;

                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            else // Si NO viene una nueva imagen
            {
                slider.UrlImagen = objFromDb.UrlImagen;
                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
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
            var sliderDesdeDb = _contenedorTrabajo.Slider.Get(id);
            string rutaDirectorioPrincipal = _hostEnvironment.WebRootPath;
            var rutaSlider = Path.Combine(rutaDirectorioPrincipal, sliderDesdeDb.UrlImagen.TrimStart('\\'));
            if (System.IO.File.Exists(rutaSlider)) // Borrar slider del directorio local
            {
                System.IO.File.Delete(rutaSlider);
            }

            if (sliderDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando Articulo" });
            }

            _contenedorTrabajo.Slider.Remove(sliderDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider borrada correctamente" });
        }

        #endregion
    }
}
