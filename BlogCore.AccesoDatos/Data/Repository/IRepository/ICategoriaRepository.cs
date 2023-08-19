using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        // Aqui van los Metodos Especificos de este Modelo

        // EL SelectListItem es para crear un DropDown y obtener dinamicamente la lista de categorias.
        IEnumerable<SelectListItem> GetListaCategorias();

        void Update(Categoria categoria);
    }
}
