using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.EntitiesCore.Sales
{
    public class SalesTransactionResponse
    {
        public int SrNo { get; set; }

        public int SaleId { get; set; }

        public string InvoiceNumber { get; set; }

        public string CustomerName { get; set; }

        public string MobileNumber { get; set; }

        public DateTime SaleDate { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public int Products { get; set; }

        public decimal NetAmount { get; set; }
    }
}
