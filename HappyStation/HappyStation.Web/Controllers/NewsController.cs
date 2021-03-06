﻿using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Constants;
using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.ControllerServices;
using HappyStation.Web.Extensions;
using HappyStation.Web.Resources;
using HappyStation.Web.Settings;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class NewsController : ControllerBase
    {
        public NewsController(NewsRepository newsRepository,
            IMappingEngine mapper,
            ApplicationSettings settings,
            IFileUploadService fileUploadService,
            InstagramService instagramService)
            : base(fileUploadService)
        {
            Contract.Requires(newsRepository != null);
            Contract.Requires(mapper != null);
            Contract.Requires(settings != null);
            Contract.Requires(instagramService != null);

            this.newsRepository = newsRepository;
            this.mapper = mapper;
            this.settings = settings;
        }

        [HttpGet, Route("article/{id}"), Route("news/{id}")]
        public ActionResult News(string id)
        {
            var news = newsRepository.Get(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            var mappedModel = mapper.Map<NewsViewModel>(news);
            // ViewBag.ShareImageSrc = mappedModel.Text.TryFindFirstImage();

            return View(mappedModel);
        }

        [HttpGet, Route("hottestnews/{count=5}")]
        public ActionResult HottestNews(int count = 5)
        {
            return View(newsRepository.GetHottest(count).Select(n => mapper.Map<NewsViewModel>(n)));
        }

        [HttpGet, Route("handmade/{count=20}")]
        public ActionResult Handmade(int count = 20)
        {
            return View(newsRepository.GetLastHandMade(count).Select(n => mapper.Map<NewsViewModel>(n)));
        }

        [HttpGet, Route("portfolio/{pageNum=1}")]
        public ActionResult Portfolio(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var news = newsRepository.GetPortfolioOnly(skip, settings.ItemsPerPage + 1).Select(n => mapper.Map<NewsViewModel>(n)).ToList();

            ViewData.Model = news.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = news.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;
            ViewBag.Title = Strings.Gallery;

            return View("List");
        }

        [HttpGet, Route("actions/{pageNum=1}")]
        public ActionResult Actions(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var news = newsRepository.GetActions(skip, settings.ItemsPerPage + 1).Select(n => mapper.Map<NewsViewModel>(n)).ToList();

            ViewData.Model = news.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = news.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;

            return View();
        }

        [HttpGet, Authorize, Route("news/admin/{pagenum=1}")]
        public ActionResult ListAdmin(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var news = newsRepository.GetBy(skip, settings.ItemsPerPage + 1).Select(n => mapper.Map<NewsViewModel>(n)).ToList();

            ViewData.Model = news.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = news.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;

            return View();
        }

        [HttpGet, Route("newslist/{pageNum=1}")]
        public ActionResult List(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var news = newsRepository.GetNewsOnly(skip, settings.ItemsPerPage + 1).Select(n => mapper.Map<NewsViewModel>(n)).ToList();

            ViewData.Model = news.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = news.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;

            return View();
        }

        [Authorize, Route("news/{id}/delete")]
        public ActionResult Delete(int id)
        {
            newsRepository.Delete(id);

            return RedirectToAction("ListAdmin");
        }

        [Authorize, Route("news/{id=0}/edit")]
        public ActionResult Edit(int id = 0)
        {
            var model = id < 1
                ? new NewsViewModel()
                : mapper.Map<NewsViewModel>(newsRepository.Get(id));

            return View(model);
        }

        [HttpPost, Authorize, Route("news/save")]
        public ActionResult Save(NewsViewModel model, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var news = new News();
            news = mapper.Map(model, news);
            if (image != null)
            {
                news.Image = UploadFile(image);
            }
            else if (!model.IsNew())
            {
                var oldNews = newsRepository.Get(model.Id);
                news.Image = oldNews.Image;
            }

            newsRepository.CreateOrUpdate(news);

            return RedirectToAction("ListAdmin");
        }

        [HttpGet]
        public ActionResult PreviewList(int count = Numbers.MaxGetCount)
        {
            ViewData.Model = newsRepository.GetNewsOnly(0, count).Select(n => mapper.Map<NewsViewModel>(n));

            return View();
        }

        private readonly NewsRepository newsRepository;
        private readonly IMappingEngine mapper;
        private readonly ApplicationSettings settings;
    }
}