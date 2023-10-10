using BlogCore.Data;
using BlogCore.Models;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Inicializador
{
    public class InicializadorDB : IInicializadorDB
    {
        /// <summary>
        /// Esto es para una Siembra de Datos, es decir, al hacer una migracion del proyecto a otro servidor y base de datos, esta
        /// clase sembrara datos en la Base de Datos nueva para que la aplicacion no comienze vacia y haya al menos un usuario
        /// administrador.
        /// </summary>

        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InicializadorDB(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Inicializar()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Count() > 0)
                {
                    _dbContext.Database.Migrate();
                }
            }
            catch (Exception)
            {
            }

            if (_dbContext.Roles.Any(role => role.Name == CNT.Admin)) return;

            // Creacion de Roles en la applicacion (aun no en la DB)
            _roleManager.CreateAsync(new IdentityRole(CNT.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Usuario)).GetAwaiter().GetResult();

            // Creacion del usuario inicial Admin en la applicacion (aun no en la DB)
            _userManager.CreateAsync(new ApplicationUser 
            {
                UserName = "admin@portfolio.com",
                Email = "admin@portfolio.com",
                EmailConfirmed = true,
                Nombre = "John Doe",

            }, "Password123+").GetAwaiter().GetResult();

            // Preparando el usuario para sembrarlo en la Db y asignandole el role Admin.
            ApplicationUser usuario = _dbContext.ApplicationUser.Where(user => user.Email == "admin@portfolio.com").FirstOrDefault();
            _userManager.AddToRoleAsync(usuario, CNT.Admin).GetAwaiter().GetResult();

            // Despues de este codigo iremos a el Program.cs y ahi se hace una Inyeccion de Esta dependencia y despues
            // se utiliza la dependencia en una implementacion de un metodo para ejecutar este metodo Inicializar().

            // Aqui termina la implementacion de codigo en el proyecto

            // Cuando se agregue una nueva Base de Datos en el Connection String se debe de ejecutar en BlogCore.AccesoDatos
            // el comando "update-database" en el NuGet CLI para subir todas las migraciones y que se creen todas las tablas
            // en la nueva Base de Datos y a parte se hace la siembra de datos, es decir en los registros ya apareceran 
            // los roles y el usuario admin.
        }
    }
}
