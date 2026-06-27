using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.ServicesCore.Customers;

namespace ERP.RepositoriesCore.Interfaces.Customers
{
    public interface IMedicineRepository
    {
        Task<List<SearchProductResponse>>SearchProducts(string searchText);
    }
}
