using ERP.RepositoriesCore.Interfaces.Customers;
using ERP.ServicesCore.Interfaces.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.ServicesCore.Customers;

namespace ERP.ServicesCore.Services.Customer
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repository;

        public MedicineService(IMedicineRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<SearchProductResponse>> SearchProducts(string searchText)
        {
            return await _repository.SearchProducts(searchText);
        }
    }
}
