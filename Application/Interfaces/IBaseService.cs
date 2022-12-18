using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Adicionar um objeto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        void Add(T objeto);

        /// <summary>
        /// Atualizar um objeto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        void Update(T objeto);

        /// <summary>
        /// Deletar um objeto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        void Delete(T objeto);

        /// <summary>
        /// Carregar um objeto com base em seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T LoadById(int id);

        /// <summary>
        /// Carregar uma lista de objetos
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> LoadAll();

        /// <summary>
        /// Carregar um objeto com base em uma expressão
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T LoadFirstBy(Expression<Func<T, bool>> predicate);

    }
}
