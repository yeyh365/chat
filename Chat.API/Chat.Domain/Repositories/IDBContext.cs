using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Repositories
{
    public interface IDBContext
    {
        IDBTransaction BeginTrainsaction();

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        //int ExecuteSqlCommand(string sql, params object[] parameters);

        //void SqlBulkInsert(string tableName, Dictionary<string, string> mapping, DataTable data, int batchSize = 10000, IDBTransaction transaction = null);
    }
}
