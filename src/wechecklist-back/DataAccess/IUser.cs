using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wechecklist_back.DataAccess.User;

namespace wechecklist_back.DataAccess
{
    public interface IUser
    {
        EnumTokenState GetUserTokenState(long userId, string token);
        Task<Token> RefreshToken(long userId, TimeSpan refreshTimeSpan, TimeSpan expiryTimeSpan);
    }
}
