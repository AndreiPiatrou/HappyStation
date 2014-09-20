using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Settings;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class NewsController : Controller
    {
        public NewsController(NewsRepository newsRepository, IMappingEngine mapper, ApplicaitonSettings settings)
        {
            Contract.Requires(newsRepository != null);
            Contract.Requires(mapper != null);
            Contract.Requires(settings != null);

            this.newsRepository = newsRepository;
            this.mapper = mapper;
            this.settings = settings;
        }

        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        public ActionResult News(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult HottestNews(int count = 4)
        {
            ViewData.Model = newsRepository.GetHottest(count).Select(n => mapper.Map<NewsViewModel>(n));

            return View();
        }

        public ActionResult ListAdmin(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            ViewData.Model = newsRepository.GetBy(skip, settings.ItemsPerPage).Select(n => mapper.Map<NewsViewModel>(n));

            return View();
        }

        private readonly NewsRepository newsRepository;
        private readonly IMappingEngine mapper;
        private readonly ApplicaitonSettings settings;
    }
}