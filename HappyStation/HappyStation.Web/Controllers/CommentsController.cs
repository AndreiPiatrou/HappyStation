using System;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Services.Implementations;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class CommentsController : Controller
    {
        public CommentsController(CommentsRepository commentsRepository, IMappingEngine mapper)
        {
            Contract.Requires(commentsRepository != null);
            Contract.Requires(mapper != null);

            this.commentsRepository = commentsRepository;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        public ActionResult LastComment()
        {
            ViewData.Model = mapper.Map<CommentVewModel>(commentsRepository.GetLast());

            return View();
        }

        private readonly CommentsRepository commentsRepository;
        private readonly IMappingEngine mapper;
    }
}