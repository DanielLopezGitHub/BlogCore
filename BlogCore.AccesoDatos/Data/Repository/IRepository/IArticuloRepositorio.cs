using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IArticuloRepository : IRepository<Articulo>
    {
        // Aqui van los Metodos Especificos de este Modelo

        void Update(Articulo categoria);
    }
}
