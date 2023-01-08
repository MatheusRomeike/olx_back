using Application.Context;
using Domain.Domain.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Atributos
        private readonly DbContextOptions<DataContext> Context;
        #endregion

        #region Construtor
        public BaseRepository()
        {
            Context = new DbContextOptions<DataContext>();
        }
        #endregion

        #region Métodos
        public virtual void Add(T objeto)
        {
            using (var context = new DataContext(Context))
            {
                context.Add(objeto);
                context.SaveChanges();
            }
        }

        public void Add(IEnumerable<T> objetos)
        {
            using (var context = new DataContext(Context))
            {
                context.AddRange(objetos);
                context.SaveChanges();
            }
        }

        public virtual IEnumerable<T> LoadAll()
        {
            using (var context = new DataContext(Context))
            {
                return context.Set<T>().ToList();
            }
        }

        public virtual void Update(T objeto)
        {
            using (var context = new DataContext(Context))
            {
                context.Update(objeto);
                context.SaveChanges();
            }
        }

        public virtual void Delete(T objeto)
        {
            using (var context = new DataContext(Context))
            {
                context.Remove(objeto);
                context.SaveChanges();
            }
        }

        public virtual T LoadById(int id)
        {
            using (var context = new DataContext(Context))
            {
                return context.Find<T>(id);
            }
        }

        public virtual T LoadFirstBy(Expression<Func<T, bool>> predicate)
        {
            using (var context = new DataContext(Context))
            {
                return context.Set<T>().Where(predicate).FirstOrDefault();
            }
        }
        #endregion
    }
}
