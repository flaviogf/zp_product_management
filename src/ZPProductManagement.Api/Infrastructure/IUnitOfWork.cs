using System.Data;

namespace ZPProductManagement.Api.Infrastructure
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void Commit();

        void Rollback();
    }
}
