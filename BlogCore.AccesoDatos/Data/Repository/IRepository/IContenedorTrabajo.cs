using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        // Aqui es donde vamos a llamar a Cada uno de los Repositorios. Cada vez que creemos un nuevo Modelo, Nueva Entidad, Nueva Tabla en DB,
        // nuevo Repositorio, tenemos que venir a añadir ese repositorio aqui.

        ICategoriaRepository Categoria { get; }
        IArticuloRepository Articulo { get; }
        ISliderRepository Slider { get; }
        IUsuarioRepository Usuario { get; }

        void Save();
    }
}
