using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Services.Implementations;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class NewsController : Controller
    {
        public NewsController(NewsRepository newsRepository, IMappingEngine mapper)
        {
            Contract.Requires(newsRepository != null);
            Contract.Requires(mapper != null);

            this.newsRepository = newsRepository;
            this.mapper = mapper;
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

        private readonly NewsRepository newsRepository;
        private readonly IMappingEngine mapper;
    }
}