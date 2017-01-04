using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wechecklist_back.DataAccess;
using wechecklist_back.DataAccess.User;

namespace wechecklist_back.Middleware
{
    public class SlidingExpiryMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUser _user;

        private static readonly TimeSpan RefreshTimeSpan = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan ExpiryTimeSpan = TimeSpan.FromDays(1);

        public SlidingExpiryMiddleware(RequestDelegate next, IUser user)
        {
            _next = next;
            _user = user;
        }

        public async Task Invoke(HttpContext context)
        {
            //await _next.Invoke(context);
            //return;

            var path = context.Request.Path;
            if (!path.StartsWithSegments(PathString.FromUriComponent("/api")))
            {
                context.Response.Cookies.Append("token", System.Uri.EscapeDataString(JsonConvert.SerializeObject(new TokenCookie { UserId = 1L, Token = "@bhsxx898ssdgg" })), new CookieOptions { HttpOnly = true, Path = "/" });
                await _next.Invoke(context);
                return;
            }
            
            var cookies = context.Request.Cookies;
            var tokenCookie = cookies["token"];
            if (string.IsNullOrEmpty(tokenCookie))
            {
                context.Response.StatusCode = StatusCodes.Status419AuthenticationTimeout;
                return;
            }
            var tokenCookieData = JsonConvert.DeserializeObject<TokenCookie>(System.Uri.UnescapeDataString(tokenCookie));
            if (tokenCookieData == null)
            {
                context.Response.StatusCode = StatusCodes.Status419AuthenticationTimeout;
                return;
            }
            var tokenState = _user.GetUserTokenState(tokenCookieData.UserId, tokenCookieData.Token);
            if (tokenState == EnumTokenState.NoUser || tokenState == EnumTokenState.Expired)
            {
                context.Response.StatusCode = StatusCodes.Status419AuthenticationTimeout;
                return;
            }
            else if (tokenState == EnumTokenState.Refresh)
            {
                Token refreshedToken = await _user.RefreshToken(tokenCookieData.UserId, RefreshTimeSpan, ExpiryTimeSpan);
                tokenCookieData = new TokenCookie { UserId = tokenCookieData.UserId, Token = refreshedToken.TokenString };
                context.Response.Cookies.Append("token", System.Uri.EscapeDataString(JsonConvert.SerializeObject(tokenCookieData)), new CookieOptions { HttpOnly = true, Path = "/", Expires = DateTimeOffset.FromFileTime(refreshedToken.ExpiryDate.ToFileTime()) });
            }
            //context.Request.Query.Append(new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("UserId", new Microsoft.Extensions.Primitives.StringValues(tokenCookieData.UserId.ToString())));
            context.Items.Add("UserId", tokenCookieData.UserId);
            await _next.Invoke(context);
        }
    }
}
