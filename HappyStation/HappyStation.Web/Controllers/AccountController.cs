using System.Collections.Generic;
using System.Web.Mvc;

using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;

using Microsoft.Web.WebPages.OAuth;

using OAuth2;
using OAuth2.Client;
using OAuth2.Models;

namespace HappyStation.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthorizationRoot authorizationRoot;
        private readonly UserRepository userRepository;
        private readonly Dictionary<string, IClient> clients;

        public AccountController(AuthorizationRoot authorizationRoot, UserRepository userRepository)
        {
            this.authorizationRoot = authorizationRoot;
            this.userRepository = userRepository;

            clients = new Dictionary<string, IClient>();
            foreach (var client in authorizationRoot.Clients)
            {
                clients.Add(client.Name.ToLower(), client);
            }
        }

        protected string ProviderName
        {
            get
            {
                return Session["providerName"].ToString();
            }

            set
            {
                Session["providerName"] = value;
            }
        }

        public ActionResult ExternalLogin(string providerName)
        {
            providerName = providerName.ToLower();
            if (!clients.ContainsKey(providerName))
            {
                return HttpNotFound();
            }

            ProviderName = providerName;
            var client = clients[ProviderName];

            return Redirect(client.GetLoginLinkUri());
        }

        public ActionResult ExternalLoginCallback()
        {
            var info = clients[ProviderName].GetUserInfo(Request.QueryString);

            if (OAuthWebSecurity.Login(ProviderName, info.Id, true))
            {
                return RedirectToAction("Index", "Home");
            }

            var user = CreateUserByInfo(info);

            OAuthWebSecurity.CreateOrUpdateAccount(ProviderName, info.Id, User.Identity.Name);
            OAuthWebSecurity.Login(ProviderName, info.Id, true);

            return RedirectToAction("Index", "Home");
        }

        private User CreateUserByInfo(UserInfo info)
        {
            var user = new User
            {
                Email = string.IsNullOrEmpty(info.Email) ? string.Format("{0}@{1}.com", info.Id, info.ProviderName.ToLower()) : info.Email,
                Name = string.Format("{0} {1}", info.FirstName, info.LastName).Trim(),
                Photo = info.AvatarUri.Large
            };

            switch (info.ProviderName.ToLower())
            {
                case "facebook":
                    user.FbId = info.Id;
                    break;
                case "twitter":
                    user.TwId = info.Id;
                    break;
                case "vk":
                case "vkontakte":
                    user.VkId = info.Id;
                    break;
            }

            return user;
        }
    }
}
