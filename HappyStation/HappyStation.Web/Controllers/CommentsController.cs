using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.ControllerServices;
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

        [HttpGet, Authorize, Route("comments/admin/{pagenum=1}")]
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
            ViewData.Model = mapper.Map<CommentVewModel>(commentsRepository.GetLast() ?? new Comment());

            return View();
        }

        [HttpGet, Authorize, Route("comment/{id}")]
        public ActionResult Comment(int id)
        {
            var comment = commentsRepository.Get(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            return View(mapper.Map<CommentVewModel>(comment));
        }

        [HttpGet, Route("comments/{pagenum=1}")]
        public ActionResult List(int pageNum)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var commants = commentsRepository.GetBy(skip, settings.ItemsPerPage + 1).Select(s => mapper.Map<CommentVewModel>(s)).ToList();

            ViewData.Model = commants.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = commants.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;

            return View();
        }

        [Authorize, Route("comment/{id}/delete")]
        public ActionResult Delete(int id)
        {
            commentsRepository.Delete(id);

            return RedirectToAction("ListAdmin");
        }

        [HttpGet, Authorize, Route("comment/{id=0}/edit")]
        public ActionResult Edit(int id = 0)
        {
            CommentVewModel model;
            if (id < 1)
            {
                model = new CommentVewModel();
            }
            else
            {
                var domainEntity = commentsRepository.Get(id);
                if (domainEntity == null)
                {
                    return HttpNotFound();
                }

                model = mapper.Map<CommentVewModel>(domainEntity);
            }

            return View(model);
        }

        [HttpPost, Authorize, Route("comment/save")]
        public ActionResult Save(CommentVewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var comment = mapper.Map<Comment>(model);
            commentsRepository.CreateOrUpdate(comment);

            return RedirectToAction("ListAdmin");
        }

        [Authorize, Route("comment/{id}/accept")]
        public ActionResult AcceptComment(int id)
        {
            var comment = commentsRepository.Get(id);
            if (comment != null)
            {
                comment.IsAccepted = true;
                commentsRepository.CreateOrUpdate(comment);
            }

            return RedirectToAction("ListAdmin");
        }

        private readonly CommentsRepository commentsRepository;
        private readonly IMappingEngine mapper;
        private readonly ApplicationSettings settings;
    }
}