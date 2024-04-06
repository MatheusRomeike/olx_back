using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Data.Contracts;
using System.Data;
using Data.Context;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Atributos
        private readonly DataContext _dataContext;
        #endregion

        #region Constructor
        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #endregion

        #region Métodos
        public bool EFCommit() => _dataContext.SaveChanges() > 0;
        public IDbContextTransaction EFBeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) => _dataContext.Database.BeginTransaction(isolationLevel);
        public void Dispose() => _dataContext.Dispose();
        #endregion

    }
}
