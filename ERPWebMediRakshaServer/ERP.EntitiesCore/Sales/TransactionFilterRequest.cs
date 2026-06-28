using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.EntitiesCore.Sales
{
    public class TransactionFilterRequest
    {
        public string? SearchText { get; set; }

        // Name or Mobile
        public string? SearchBy { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 15;
    }
}
