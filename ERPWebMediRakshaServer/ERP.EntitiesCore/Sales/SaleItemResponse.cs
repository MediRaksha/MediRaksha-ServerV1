using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.EntitiesCore.Sales
{
    public class SaleItemResponse
    {
        public int SaleItemId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Rate { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal Amount { get; set; }
    }
}
