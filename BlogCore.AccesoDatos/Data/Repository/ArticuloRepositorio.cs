using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    internal class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Articulo articulo)
        {
            // Obtenemos el Objeto Articulo desde DB por medio del Id del obj nuevo que nos llega desde Parametros
            var objDesdeDb = _db.Articulos.FirstOrDefault(s => s.Id == articulo.Id);

            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;

            //_db.SaveChanges(); El Guargadro para Articulo se realizara en el Contenedor de Trabajo
        }
    }
}
