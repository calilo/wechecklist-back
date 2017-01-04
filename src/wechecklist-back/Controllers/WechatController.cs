using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Microsoft.Extensions.Options;
using wechecklist_back.Tools;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wechecklist_back.Controllers
{
    public class WechatController : Controller
    {
        private readonly AppConfig _config;
        private string appId { get { return _config.Wechat.AppID; } }
        private string appSecret { get { return _config.Wechat.AppSecret; } }

        public WechatController(IOptions<AppConfig> config)
        {
            _config = config.Value;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var redirectUrl = OAuthApi.GetAuthorizeUrl(appId, "http://hololensObvious.com/wechat/userinfo", "", Senparc.Weixin.MP.OAuthScope.snsapi_userinfo);
            return Redirect(redirectUrl);
        }
        [HttpGet]
        public ActionResult UserInfo(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
            {
                return Content("您拒绝了授权！");
            }
            
            try
            {
                var accessToken = OAuthApi.GetAccessToken(appId, appSecret, code);
                var userInfo = OAuthApi.GetUserInfo(accessToken.access_token, accessToken.openid);
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
