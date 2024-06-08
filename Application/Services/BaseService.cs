using Application.Interfaces;
using Domain.Core.Contracts;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        #region Atributos
        protected readonly IBaseRepository<T> _repository;
        #endregion

        #region Construtor
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Métodos
        public virtual void Add(T objeto)
        {
            _repository.Add(objeto);
        }

        public virtual void Update(T objeto)
        {
            _repository.Update(objeto);
        }

        public virtual void Delete(T objeto)
        {
            _repository.Delete(objeto);
        }

        public virtual T LoadById(int id)
        {
            return _repository.LoadById(id);
        }

        public virtual T? LoadFirstBy(
                    Expression<Func<T, bool>>? predicate = null,
                    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                    Expression<Func<T, T>>? selector = null)
        {
            return _repository.LoadFirstBy(predicate, include, selector);
        }

        public virtual T? LoadLastBy(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, T>>? selector = null)
        {
            return _repository.LoadLastBy(predicate, include, orderBy, selector);
        }

        public virtual IEnumerable<T> LoadAll(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            int? limit = null,
            int? skip = null,
            Expression<Func<T, T>>? selector = null)
        {
            return _repository.LoadAll(predicate, include, limit, skip, selector);
        }

        public void PartialUpdate(T entity, params Expression<Func<T, object>>[] properties)
        {
            _repository.PartialUpdate(entity, properties);
        }


        #endregion
    }
}
