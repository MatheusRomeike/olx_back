using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Data.Contracts
{
    public interface IUnitOfWork
    {
        bool EFCommit();
        IDbContextTransaction EFBeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        void Dispose();
    }
}
