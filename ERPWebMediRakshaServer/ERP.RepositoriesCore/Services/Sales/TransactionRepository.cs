using ERP.DataCore.Interfaces;
using ERP.EntitiesCore.Sales;
using ERP.RepositoriesCore.Interfaces.Sales;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.RepositoriesCore.Services.Sales
{
    public class TransactionRepository
        : ITransactionRepository
    {
        private readonly IDbManager _dbManager;

        public TransactionRepository(
            IDbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<List<SalesTransactionResponse>> GetTransactions(int pageNumber, int pageSize)
        {
            SqlParameter[] parameters =
            {
                new("@PageNumber", pageNumber),
                new("@PageSize", pageSize)
            };
            return await _dbManager.GetListAsync<SalesTransactionResponse>("sp_GetSalesTransactions", parameters);
        }
        public async Task<SaleDetailsResponse> GetSaleDetails(int saleId)
        {
            SqlParameter[] parameters =
            {
                new("@SaleId", saleId)
            };
            var result = await _dbManager.GetMultipleAsync<SaleHeaderResponse, SaleItemResponse>("sp_GetSaleDetails", parameters);
            return new SaleDetailsResponse
            {
                Header = result.Item1,
                Items = result.Item2
            };
        }
        public async Task<List<SalesTransactionResponse>> FilterTransactions(TransactionFilterRequest request)
        {
            SqlParameter[] parameters =
            {
                    new("@SearchText",(object?)request.SearchText?? DBNull.Value),
                    new("@SearchBy",(object?)request.SearchBy ?? DBNull.Value),
                    new("@FromDate",(object?)request.FromDate?? DBNull.Value),
                    new("@ToDate",(object?)request.ToDate ?? DBNull.Value),
                    new("@PageNumber",request.PageNumber),
                    new("@PageSize", request.PageSize)
                };

            return await _dbManager.GetSafeListAsync<SalesTransactionResponse>(
    "sp_FilterTransactions",
    parameters);
        }
    }
}
