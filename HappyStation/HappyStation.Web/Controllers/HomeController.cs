using System.Web.Mvc;

using HappyStation.Core.DatabaseContext;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Resources;

namespace HappyStation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsRepository newsRepository;
        private readonly EventsRepository eventsRepository;

        public HomeController()
        {
            var dataContext = new DatabaseContext();
            newsRepository = new NewsRepository(dataContext);
            eventsRepository = new EventsRepository(dataContext);
        }

        public HomeController(NewsRepository newsRepository, EventsRepository eventsRepository)
        {
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

        //[Authorize]
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult About()
        {
            throw new System.NotImplementedException();
        }
    }
}
