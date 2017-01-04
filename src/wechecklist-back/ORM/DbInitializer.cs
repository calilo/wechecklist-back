using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wechecklist_back.DataAccess.Common;
using wechecklist_back.ORM.Models;

namespace wechecklist_back.ORM
{
    public static class DbInitializer
    {
        public static void Initialize(ChecklistDBContext context)
        {
            if (context.Database.EnsureCreated())
            {
                context.Users.Add(new User { HeadImgUrl = "defaultImg.png", UserName = "sunz", Token = "@bhsxx898ssdgg", ExpiryDate = DateTime.Parse("2018/01/01"), RefreshDate = DateTime.Parse("2017/10/01"), OpenId = "bhsxx898ssdgg", UserId = 1, UserType = (int)EnumUserUserType.NormalUser, CreateTime = DateTime.Now, CreateUserId = 1 });
                context.SaveChanges();
            }
        }
    }
}
