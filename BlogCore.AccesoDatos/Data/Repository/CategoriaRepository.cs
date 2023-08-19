using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlogCore.AccesoDatos.Data.Repository
{
    internal class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categorias.Select(i => new SelectListItem() { Text = i.Nombre, Value = i.Id.ToString() });
        }

        public void Update(Categoria categoria)
        {
            // Obtenemos el Objeto Categoria desde DB por medio del Id del obj nuevo que nos llega desde Parametros
            var objDesdeDb = _db.Categorias.FirstOrDefault(s => s.Id == categoria.Id);

            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;

            _db.SaveChanges();
        }
    }
}
