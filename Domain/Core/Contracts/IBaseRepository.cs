using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Domain.Core.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Adicionar um objeto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        void Add(T objeto);

        /// <summary>
        /// Adicionar uma lista de objetos
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        void Add(IEnumerable<T> objeto);

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
        /// Método responsável por carregar o primeiro baseado nas condiçoes passadas
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        T? LoadFirstBy(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Expression<Func<T, T>>? selector = null);

        /// <summary>
        /// Método responsável por carregar o ultimo baseado nas condiçoes passadas
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="orderBy"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        T? LoadLastBy(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Expression<Func<T, T>>? selector = null);

        /// <summary>
        /// Método responsável por carregar todos baseados nas condiçoes passadas
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="limit"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        IEnumerable<T> LoadAll(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? limit = null,
            int? skip = null,
            Expression<Func<T, T>>? selector = null);

        /// <summary>
        /// Método responsável por buscar baseado nas condiçoes passadas
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <param name="orderBy"></param>
        /// <param name="limit"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        IQueryable<T> GetQuery(
            Expression<Func<T, bool>>? predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            int? limit,
            int? skip = null,
            Expression<Func<T, T>>? selector = null);

        /// <summary>
        /// Método responsável por atualizar a entidade baseado nas condiçoes passadas
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        void PartialUpdate(T entity, params Expression<Func<T, object>>[] properties);
    }
}
