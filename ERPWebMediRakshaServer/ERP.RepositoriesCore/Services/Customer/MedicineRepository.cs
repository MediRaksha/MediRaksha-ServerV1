using ERP.DataCore.Interfaces;
using ERP.RepositoriesCore.Interfaces.Customers;
using ERP.ServicesCore.Customers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.RepositoriesCore.Customer
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly IDbManager _dbManager;

        public MedicineRepository(IDbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<List<SearchProductResponse>> SearchProducts(string searchText)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@ProductName",searchText ?? (object)DBNull.Value)
            };

            return await _dbManager.GetListAsync<SearchProductResponse>("sp_GetProductDataByName", parameters);
        }
    }
}
