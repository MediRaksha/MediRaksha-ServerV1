using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ERP.ServicesCore.Customers;

namespace ERP.ServicesCore.Interfaces.Customers
{
    public interface IMedicineService
    {
        Task<List<SearchProductResponse>> SearchProducts(string? searchText);
    }
}
