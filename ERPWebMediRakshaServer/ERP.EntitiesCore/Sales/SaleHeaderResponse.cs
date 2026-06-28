using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.EntitiesCore.Sales
{
    public class SaleHeaderResponse
    {
        public int SaleId { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime SaleDate { get; set; }

        public string FullName { get; set; }

        public string MobileNumber { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal NetAmount { get; set; }
    }
}
