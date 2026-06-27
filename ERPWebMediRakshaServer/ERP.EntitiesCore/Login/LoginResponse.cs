using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.EntitiesCore.Login
{
    public class LoginResponse
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
