using ERP.EntitiesCore.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ServicesCore.Interfaces.Login
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
