using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DataCore.Interfaces
{
    public interface IDbManager
    {
        Task<List<T>> GetListAsync<T>(string procedureName, SqlParameter[] parameters) where T : new();
        Task<int> ExecuteAsync(string procedureName, SqlParameter[] parameters);
    }
}
