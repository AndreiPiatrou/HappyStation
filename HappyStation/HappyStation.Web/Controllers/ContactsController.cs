using System.Diagnostics.Contracts;
using System.Web.Mvc;

using HappyStation.Web.ControllerServices;
using HappyStation.Web.Settings;

namespace HappyStation.Web.Controllers
{
    public class ContactsController : ControllerBase
    {
        public ContactsController(FileUploadService uploadService,
            InstagramService instagramService,
            ApplicationSettings settings)
            : base(uploadService)
        {
            Contract.Requires(instagramService != null);
            Contract.Requires(settings != null);

            this.instagramService = instagramService;
            this.settings = settings;
        }
        
        public ActionResult GetInstagramFeed()
        {
            ViewData.Model = instagramService.GetFeed(9);
            ViewBag.InstagramLink = "http://instagram.com/" + settings.InstagramUserName;

            return View();
        }

        public ActionResult WeInSocialNetworks()
        {
            ViewBag.FacebookLink = settings.FacebookLink;
            ViewBag.VkontakteLink = settings.VkontakteLink;
            ViewBag.InstagramLink = settings.InstagramLink;

            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult ShareThis()
        {
            return View();
        }

        private readonly InstagramService instagramService;
        private readonly ApplicationSettings settings;
    }
}