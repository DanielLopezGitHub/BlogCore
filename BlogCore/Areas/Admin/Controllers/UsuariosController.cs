using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        // - - - --  - - - - - - Inyecciones de Dependencias - - - - - - - - -
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo, ApplicationDbContext contexto)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }
        // - - - --  - - - - - - Inyecciones de Dependencias - - - - - - - - -

        [HttpGet]
        public IActionResult Index()
        {
            // Opcion 1: Obtener todos lo usuarios
            //return View(_contenedorTrabajo.Usuario.GetAll());

            // Opcion 2: Permite obtener todos los usuarios menos el que esta autenticado para no bloquearse a si mismo
            // La sig linea obtiene toda la informacion de autenticacion relacionada al usuario autenticado.
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            // Obtenemos al usuario por su identificador...
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            // Le decimos al manejador de la DB que nos traida todos los Usuarios menos al usuario actual autenticado.
            return View(_contenedorTrabajo.Usuario.GetAll(user => user.Id != usuarioActual.Value));
        }

        [HttpGet]
        public IActionResult Bloquear(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.BloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.DesbloquearUsuario(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
