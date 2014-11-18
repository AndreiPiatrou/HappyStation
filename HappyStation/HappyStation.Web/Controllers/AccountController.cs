using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Resources;
using HappyStation.Web.ViewModels;

using Microsoft.Web.WebPages.OAuth;

using OAuth2;
using OAuth2.Client;
using OAuth2.Models;

using WebMatrix.WebData;

namespace HappyStation.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(AuthorizationRoot authorizationRoot, UserRepository userRepository)
        {
            Contract.Requires(authorizationRoot != null);
            Contract.Requires(userRepository != null);

            this.authorizationRoot = authorizationRoot;
            this.userRepository = userRepository;

            clients = new Dictionary<string, IClient>();
            foreach (var client in this.authorizationRoot.Clients)
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

        [HttpGet, Route("login")]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken, Route("login")]
        public ActionResult Login(LoginViewModel model)
        {
            var user = userRepository.GetByLogin(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, Strings.UserNotFound);
                return View(model);
            }

            if (WebSecurity.Login(model.Email, model.Password, true))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
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

            OAuthWebSecurity.CreateOrUpdateAccount(ProviderName, info.Id, User.Identity.Name);
            OAuthWebSecurity.Login(ProviderName, info.Id, true);
            userRepository.CreateOrUpdate(CreateUserByInfo(info));

            return RedirectToAction("Index", "Home");
        }

        [Route("logout")]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

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

        private readonly AuthorizationRoot authorizationRoot;
        private readonly UserRepository userRepository;
        private readonly Dictionary<string, IClient> clients;
    }
}
