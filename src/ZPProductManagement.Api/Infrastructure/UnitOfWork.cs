using System;
using System.Data;

namespace ZPProductManagement.Api.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        public UnitOfWork(IDbConnection connection)
        {
            Connection = connection;

            connection.Open();

            Transaction = connection.BeginTransaction();
        }

        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; }

        public void Commit()
        {
            Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            Transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            Transaction.Dispose();
            Connection.Dispose();
        }
    }
}
