using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Aqui es donde usaremos el DbContext para hasta este punto comunicarnos con la Base de Datos y manejarla.

        // Miembros:
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        // Constructor:
        public Repository(DbContext context)
        {
            // Recibimos la Dependencia "DbContext" Inyectada desde "Program".
            Context = context;
            this.dbSet = context.Set<T>();
        }

        // Metodos Genericos:
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            // Si viene algun filtro definido como argumento, vamos a procesarlo:
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include Properties separados por coma:
            // Estas Include Properties es cuando hay relacion 1 a 1, 1 a muchos y muchos a muchos, y desde esta tabla queremos llamar a registros de otras tablas.
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // El DbSet.Include() nos permite traer datos de varias entidades relacionadas.
                    query = query.Include(includeProperty);
                }
            }

            // Si viene una expresion de ordenamiento la procesaremos aqui.
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            // Si viene algun filtro definido como argumento, vamos a procesarlo:
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include Properties separados por coma:
            // Estas Include Properties es cuando hay relacion 1 a 1, 1 a muchos y muchos a muchos, y desde esta tabla queremos llamar a registros de otras tablas.
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    // El DbSet.Include() nos permite traer datos de varias entidades relacionadas.
                    query = query.Include(includeProperty);
                }
            }

            // El Metodo DbSet.FirstOrDefault nos permite traer solo un registro.
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
        }
    }
}
