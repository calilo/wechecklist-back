using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wechecklist_back.DataAccess.User;
using wechecklist_back.ORM;

namespace wechecklist_back.DataAccess
{
    public class DefaultUser : IUser
    {
        private readonly ChecklistDBContext _dbContext;
        public DefaultUser(ChecklistDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        EnumTokenState IUser.GetUserTokenState(long userId, string token)
        {
            DateTime now = DateTime.Now;
            var user = _dbContext.Users.FirstOrDefault(p => p.UserId == userId);
            if (user == null)
            {
                return EnumTokenState.NoUser;
            }
            else if (user.Token != token || user.ExpiryDate <= now)
            {
                return EnumTokenState.Expired;
            }
            else if (user.RefreshDate <= now)
            {
                return EnumTokenState.Refresh;
            }
            else
            {
                return EnumTokenState.Valid;
            }
        }

        async Task<Token> IUser.RefreshToken(long userId, TimeSpan refreshTimeSpan, TimeSpan expiryTimeSpan)
        {
            DateTime now = DateTime.Now;
            Guid tokenGuid = Guid.NewGuid();
            var user = _dbContext.Users.First(p => p.UserId == userId);
            user.Token = tokenGuid.ToString();
            user.ExpiryDate = now + expiryTimeSpan;
            await _dbContext.SaveChangesAsync();
            return new Token { UserId = userId, TokenString = user.Token, ExpiryDate = user.ExpiryDate };
        }
    }
}
