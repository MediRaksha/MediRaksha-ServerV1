using ERP.EntitiesCore.Login;
using ERP.RepositoriesCore.Interfaces.Login;
using ERP.RepositoriesCore.Services.Login;
using ERP.ServicesCore.Interfaces.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ServicesCore.Services.Login
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly JwtTokenGenerator _jwt;
        public AuthService(IAuthRepository repository, JwtTokenGenerator jwt)
        {
            _repository = repository;
            _jwt = jwt;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _repository.GetUserByEmail(request.Email);
            if (user != null&& user.Email==request.Email && user.Password == request.Password)
            {
                var accessToken = _jwt.GenerateToken(user);
                var refreshToken = _jwt.GenerateRefreshToken();
                await _repository.SaveRefreshToken(user.UserId, refreshToken, DateTime.Now.AddDays(7));
                return new LoginResponse
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Role = user.Role,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };                
            }
            else { throw new Exception("Invalid Password"); }
            
        }
    }
}
