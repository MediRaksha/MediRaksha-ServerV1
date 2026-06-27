using ERP.EntitiesCore.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.RepositoriesCore.Interfaces.Login
{
    public interface IAuthRepository
    {
        Task<UserModel> GetUserByEmail(string email);

        Task SaveRefreshToken(
            int userId,
            string refreshToken,
            DateTime expiryDate);

        Task<RefreshTokenModel>
            GetRefreshToken(string refreshToken);
    }
}
