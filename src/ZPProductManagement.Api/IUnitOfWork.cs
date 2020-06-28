using System.Data;

namespace ZPProductManagement.Api
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void Commit();

        void Rollback();
    }
}
