using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Services;
using HappyStation.Web.Settings;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class CommentsController : ControllerBase
    {
        public CommentsController(CommentsRepository commentsRepository,
            IMappingEngine mapper,
              ApplicationSettings settings,
            FileUploadService fileUploadService)
            : base(fileUploadService)
        {
            Contract.Requires(commentsRepository != null);
            Contract.Requires(mapper != null);
            Contract.Requires(settings != null);

            this.commentsRepository = commentsRepository;
            this.mapper = mapper;
            this.settings = settings;
        }

        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        [Authorize]
        public ActionResult ListAdmin(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var comments = commentsRepository.GetBy(skip, settings.ItemsPerPage + 1).Select(n => mapper.Map<CommentVewModel>(n)).ToList();

            ViewData.Model = comments.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = comments.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;

            return View();
        }

        public ActionResult LastComment()
        {
            ViewData.Model = mapper.Map<CommentVewModel>(commentsRepository.GetLast());

            return View();
        }

        private readonly CommentsRepository commentsRepository;
        private readonly IMappingEngine mapper;
        private readonly ApplicationSettings settings;

        public ActionResult Edit()
        {
            throw new NotImplementedException();
        }

        public ActionResult Comment(int id)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            commentsRepository.Delete(id);

            return RedirectToAction("ListAdmin");
        }
    }
}