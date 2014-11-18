using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.ControllerServices;
using HappyStation.Web.Extensions;
using HappyStation.Web.Settings;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class GalleryController : ControllerBase
    {
        public GalleryController(FileUploadService uploadService,
            PhotoAlbumService photoAlbumService,
            IMappingEngine mapper,
            ApplicationSettings settings,
            PhotoService photoService)
            : base(uploadService)
        {
            Contract.Requires(uploadService != null);
            Contract.Requires(photoAlbumService != null);
            Contract.Requires(mapper != null);
            Contract.Requires(settings != null);
            Contract.Requires(photoService != null);

            this.photoAlbumService = photoAlbumService;
            this.mapper = mapper;
            this.settings = settings;
            this.photoService = photoService;
        }

        [HttpGet, Authorize]
        public ActionResult EditAlbum(int id = 0)
        {
            var model = new PhotoAlbumViewModel();
            var album = photoAlbumService.Get(id);
            if (album != null)
            {
                model = mapper.Map<PhotoAlbumViewModel>(album);
            }

            ViewData.Model = model;

            return View();
        }

        [HttpPost, Authorize]
        public ActionResult Save(PhotoAlbumViewModel model, HttpPostedFileBase[] newPhotos)
        {
            if (!ModelState.IsValid)
            {
                return View("EditAlbum", model);
            }

            var album = mapper.Map<PhotoAlbum>(model);
            foreach (var httpPostedFileBase in newPhotos)
            {
                album.Photos.Add(new Photo
                {
                    Path = UploadFile(httpPostedFileBase)
                });
            }
            if (!model.IsNew())
            {
                var oldAlbum = photoAlbumService.Get(model.Id);
                album.Photos.AddRange(oldAlbum.Photos);
            }

            photoAlbumService.CreateOrUpdate(album);

            return RedirectToAction("ListAdmin");
        }

        [HttpGet, Authorize, Route("albums/admin/{pagenum=1}")]
        public ActionResult ListAdmin(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var news = photoAlbumService.GetBy(skip, settings.ItemsPerPage + 1).Select(n => mapper.Map<PhotoAlbumViewModel>(n)).ToList();

            ViewData.Model = news.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = news.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;

            return View();
        }

        [HttpGet, Route("albums/{pageNum=1}")]
        public ActionResult List(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var albums = photoAlbumService.GetBy(skip, settings.ItemsPerPage + 1).Select(n => mapper.Map<PhotoAlbumViewModel>(n)).ToList();

            ViewData.Model = albums.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = albums.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;

            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            photoAlbumService.Delete(id);

            return RedirectToAction("ListAdmin");
        }

        [HttpGet, Route("album/{id}")]
        public ActionResult Album(int id)
        {
            var album = photoAlbumService.Get(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            ViewData.Model = mapper.Map<PhotoAlbumViewModel>(album);

            return View();
        }
        [Authorize]
        public ActionResult DeletePhoto(int id, int albumId)
        {
            photoService.Delete(id);

            return RedirectToAction("EditAlbum",
                new
                {
                    @id = albumId
                });
        }

        private readonly PhotoAlbumService photoAlbumService;
        private readonly IMappingEngine mapper;
        private readonly ApplicationSettings settings;
        private readonly PhotoService photoService;
    }
}