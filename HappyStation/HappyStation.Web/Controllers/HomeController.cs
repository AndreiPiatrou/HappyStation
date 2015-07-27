using System.Diagnostics.Contracts;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Resources;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(NewsRepository newsRepository,
            EventsRepository eventsRepository,
            PageContentRepository pageContentRepository,
            IMappingEngine mapper)
        {
            Contract.Requires(newsRepository != null);
            Contract.Requires(eventsRepository != null);
            Contract.Requires(pageContentRepository != null);
            Contract.Requires(mapper != null);

            this.newsRepository = newsRepository;
            this.eventsRepository = eventsRepository;
            this.pageContentRepository = pageContentRepository;
            this.mapper = mapper;

            ViewBag.Title = Strings.MainPage;
        }

        public ActionResult Index()
        {
            ViewBag.News = newsRepository.GetBy();
            ViewBag.Events = eventsRepository.GetBy();

            var pageContent = pageContentRepository.GetByType(PageContentType.About);
            ViewBag.AboutUs = pageContent != null ? pageContent.Text : string.Empty;
            
            return View();
        }

        [Authorize, Route("admin")]
        public ActionResult Admin()
        {
            return View();
        }

        [Route("aboutus")]
        public ActionResult About()
        {
            var pageContent = pageContentRepository.GetByType(PageContentType.About);

            return View(pageContent != null ? mapper.Map<PageContentViewModel>(pageContent) : null);
        }

        [Route("price")]
        public ActionResult Price()
        {
            var pageContent = pageContentRepository.GetByType(PageContentType.Price);

            return View(pageContent != null ? mapper.Map<PageContentViewModel>(pageContent) : null);
        }

        private readonly NewsRepository newsRepository;
        private readonly EventsRepository eventsRepository;
        private readonly PageContentRepository pageContentRepository;
        private readonly IMappingEngine mapper;
    }
}
