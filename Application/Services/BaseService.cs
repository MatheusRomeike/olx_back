using Domain.Domain.Core.Contracts;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

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

        public virtual IEnumerable<T> LoadAll()
        {
            return _repository.LoadAll();
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

        public virtual T LoadFirstBy(Expression<Func<T, bool>> predicate)
        {
            return _repository.LoadFirstBy(predicate);
        }

        #endregion
    }
}
