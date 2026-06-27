using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.EntitiesCore.Login
{
    public class RefreshTokenModel
    {
        public int UserId { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }
    }
}
