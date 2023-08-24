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
    internal class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Slider slider)
        {
            // Obtenemos el Objeto Slider desde DB por medio del Id del obj nuevo que nos llega desde Parametros
            var objDesdeDb = _db.Sliders.FirstOrDefault(s => s.Id == slider.Id);

            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;

            //_db.SaveChanges(); El Guargadro para Slider se realizara en el Contenedor de Trabajo
        }
    }
}
