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
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _dbContext;
        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _dbContext = db;
            Categoria = new CategoriaRepository(_dbContext);
            Articulo = new ArticuloRepository(_dbContext);
            Slider = new SliderRepository(_dbContext);
            Usuario = new UsuarioRepository(_dbContext);
        }

        public ICategoriaRepository Categoria { get; set; }
        public IArticuloRepository Articulo { get; set; }
        public ISliderRepository Slider { get; set; }
        public IUsuarioRepository Usuario { get; set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
