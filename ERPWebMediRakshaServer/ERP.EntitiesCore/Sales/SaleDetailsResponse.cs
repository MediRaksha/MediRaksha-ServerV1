using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.EntitiesCore.Sales
{
    public class SaleDetailsResponse
    {
        public SaleHeaderResponse Header { get; set; }

        public List<SaleItemResponse> Items { get; set; }
    }
}
