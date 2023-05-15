using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chat.Domain.Repositories;

namespace Chat.EntityFrameworkCore.Repositories
{
    public class EFDBTransaction : IDBTransaction
    {
        private IDbContextTransaction _transaction;

        public EFDBTransaction(DbContext context)
        {
            _transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            this._transaction.Commit();
        }

        public void Rollback()
        {
            this._transaction.Rollback();
        }

        public void Dispose()
        {
            this._transaction.Dispose();
        }

        public IDbContextTransaction GetTransaction()
        {
            return _transaction;
        }
    }
}
