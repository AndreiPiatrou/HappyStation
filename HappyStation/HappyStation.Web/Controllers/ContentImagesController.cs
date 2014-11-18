using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.ControllerServices;
using HappyStation.Web.Settings;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class ContentImagesController : ControllerBase
    {
        public ContentImagesController(FileUploadService uploadService,
            ContentImagesService contentImagesService,
            ApplicationSettings settings,
            IMappingEngine mapper)
            : base(uploadService)
        {
            Contract.Requires(contentImagesService != null);
            Contract.Requires(settings != null);
            Contract.Requires(mapper != null);

            this.contentImagesService = contentImagesService;
            this.settings = settings;
            this.mapper = mapper;
        }

        [Authorize]
        public ActionResult Upload(HttpPostedFileBase[] images)
        {
            var newImages = images.Select(httpPostedFileBase => new ContentImage
            {
                Path = UploadFile(httpPostedFileBase)
            }).ToList();

            contentImagesService.CreateOrUpdate(newImages);

            return RedirectToAction("ListAdmin");
        }

        [Authorize]
        public ActionResult ListAdmin(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var images = contentImagesService.GetBy(skip, settings.ItemsPerPage + 1).Select(s => mapper.Map<ContentImageViewModel>(s)).ToList();

            ViewData.Model = images.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = images.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;

            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var image = contentImagesService.Get(id);
            if (image != null)
            {
                DeleteFile(image.Path);
                contentImagesService.Delete(id);
            }

            return RedirectToAction("ListAdmin");
        }

        private readonly ContentImagesService contentImagesService;
        private readonly ApplicationSettings settings;
        private readonly IMappingEngine mapper;
    }
}