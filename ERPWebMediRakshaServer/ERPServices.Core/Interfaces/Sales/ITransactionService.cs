using ERP.EntitiesCore.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ServicesCore.Interfaces.Sales
{
    public interface ITransactionService
    {
        Task<List<SalesTransactionResponse>> GetTransactions(int pageNumber, int pageSize);
        Task<SaleDetailsResponse> GetSaleDetails(int saleId);
        Task<List<SalesTransactionResponse>> FilterTransactions(TransactionFilterRequest request);

    }
}
