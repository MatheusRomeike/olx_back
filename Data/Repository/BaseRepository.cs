using Application.Context;
using Domain.Core.Contracts;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        #region Atributos
        private readonly DataContext context;
        #endregion

        #region Construtor
        public BaseRepository()
        {
            context = new DataContext();
        }
        #endregion

        #region Métodos
        public virtual void Add(T objeto)
        {
            context.Add(objeto);
            context.SaveChanges();
        }

        public void Add(IEnumerable<T> objetos)
        {
            context.AddRange(objetos);
            context.SaveChanges();
        }

        public virtual IEnumerable<T> LoadAll()
        {
            return context.Set<T>().ToList();
        }

        public virtual void Update(T objeto)
        {
            context.Update(objeto);
            context.SaveChanges();
        }

        public virtual void Delete(T objeto)
        {
            context.Remove(objeto);
            context.SaveChanges();
        }

        public virtual T LoadById(int id)
        {
            return context.Find<T>(id);
        }

        public T? LoadFirstBy(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Expression<Func<T, T>>? selector = null)
        {
            var query = GetQuery(predicate: predicate, limit: 1, include: include, selector: selector);
            return query.FirstOrDefault();
        }

        public virtual T? LoadLastBy(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, T>>? selector = null)
        {
            var query = GetQuery(predicate, include, limit: 1, orderBy: orderBy, selector: selector);
            return query.LastOrDefault();
        }

        public virtual IEnumerable<T> LoadAll(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int? limit = null,
            Expression<Func<T, T>>? selector = null)
        {
            var query = GetQuery(predicate: predicate, limit: limit, include: include, selector: selector);
            return query.ToList();
        }

        public IQueryable<T> GetQuery(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? limit = null,
            Expression<Func<T, T>>? selector = null)
        {
            IQueryable<T> query = context.Set<T>().AsQueryable();

            if (predicate != null)
                query = query.Where(predicate);

            if (limit != null)
                query = query.Take(limit.Value);

            if (orderBy != null)
                query = orderBy(query);

            if (include != null)
            {
                query = include(query);
            }

            if (selector != null)
            {
                query = query.Select(selector).AsQueryable();
            }

            return query;
        }
        public void PartialUpdate(T entity, params Expression<Func<T, object>>[] properties)
        {
            var propertyNames = properties.Select(i => i.Name).ToArray();
            PartialUpdate(entity, propertyNames);
        }

        public void PartialUpdate(T entity, string[] properties)
        {
            foreach (var property in properties)
            {
                context.Entry(entity).Property(property).IsModified = true;
            }
        }
        #endregion
    }
}
