using System.Diagnostics.Contracts;
using System.Web.Mvc;

using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Resources;

namespace HappyStation.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(NewsRepository newsRepository, EventsRepository eventsRepository)
        {
            Contract.Requires(newsRepository != null);
            Contract.Requires(eventsRepository != null);

            this.newsRepository = newsRepository;
            this.eventsRepository = eventsRepository;

            ViewBag.Title = Strings.MainPage;
        }

        public ActionResult Index()
        {
            ViewBag.News = newsRepository.GetBy();
            ViewBag.Events = eventsRepository.GetBy();

            return View();
        }

        [Authorize]
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        private readonly NewsRepository newsRepository;
        private readonly EventsRepository eventsRepository;

    }
}
