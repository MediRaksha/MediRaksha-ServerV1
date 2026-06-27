using ERP.DataCore.Interfaces;
using ERP.EntitiesCore.Login;
using ERP.RepositoriesCore.Interfaces.Login;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.RepositoriesCore.Services.Login
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbManager _dbManager;

        public AuthRepository(IDbManager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email)
            };

            var users = await _dbManager.GetListAsync<UserModel>("sp_GetUserByEmail", parameters);
            return users.FirstOrDefault();
        }
        public async Task SaveRefreshToken(int userId, string refreshToken, DateTime expiryDate)
        {
            SqlParameter[] parameters =
            {
                new("@UserId", userId),
                new("@RefreshToken", refreshToken),
                new("@ExpiryDate", expiryDate)
            };

            await _dbManager.ExecuteAsync("sp_SaveRefreshToken", parameters);
        }
        public async Task<RefreshTokenModel> GetRefreshToken(string refreshToken)
        {
            SqlParameter[] parameters =
            {
                new("@RefreshToken", refreshToken)
            };
            var result = await _dbManager.GetListAsync<RefreshTokenModel>("sp_GetRefreshToken", parameters);
            return result.FirstOrDefault();
        }

    }

}
