using ERP.EntitiesCore.Sales;
using ERP.RepositoriesCore.Interfaces.Sales;
using ERP.ServicesCore.Interfaces.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ServicesCore.Services.Sales
{
    public class TransactionService
    : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(
            ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SalesTransactionResponse>> GetTransactions(int pageNumber, int pageSize)
        {
            return await _repository.GetTransactions(pageNumber, pageSize);
        }
        public async Task<SaleDetailsResponse> GetSaleDetails(int saleId)
        {
            return await _repository.GetSaleDetails(saleId);
        }
        public async Task<List<SalesTransactionResponse>> FilterTransactions(TransactionFilterRequest request)
        {
            return await _repository.FilterTransactions(request);
        }
    }

}
