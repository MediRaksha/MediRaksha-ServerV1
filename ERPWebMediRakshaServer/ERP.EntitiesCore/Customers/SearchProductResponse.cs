using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ServicesCore.Customers
{
    public class SearchProductResponse
    {
        public int MedicineId { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public string Category { get; set; }

        public decimal MRP { get; set; }

        public decimal RateA { get; set; }

        public string RackNumber { get; set; }

        public string Manufacturer { get; set; }
    }
}
