using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Extensions;
using HappyStation.Web.Services;
using HappyStation.Web.Settings;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class ServicesController : ControllerBase
    {
        public ServicesController(ServicesRepository servicesRepository,
            IMappingEngine mapper,
            ApplicationSettings settings,
            FileUploadService fileUploadService)
            : base(fileUploadService)
        {
            Contract.Requires(servicesRepository != null);
            Contract.Requires(mapper != null);
            Contract.Requires(settings != null);

            this.servicesRepository = servicesRepository;
            this.mapper = mapper;
            this.settings = settings;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult HotestServices(int count = 4)
        {
            ViewData.Model = servicesRepository.GetHottest(count).Select(s => mapper.Map<ServiceViewModel>(s));

            return View();
        }

        public ActionResult Service(int id)
        {
            ViewData.Model = mapper.Map<ServiceViewModel>(servicesRepository.Get(id));

            return View();
        }

        [HttpGet]
        public ActionResult List(int pageNum = 1)
        {
            FillListViewModel(pageNum);

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ListAdmin(int pageNum = 1)
        {
            FillListViewModel(pageNum);

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id = 0)
        {
            var model = id < 1
                ? new ServiceViewModel()
                : mapper.Map<ServiceViewModel>(servicesRepository.Get(id));

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Save(ServiceViewModel model, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            
            var newService = mapper.Map<Service>(model);
            if (image != null)
            {
                newService.Image = UploadFile(image);
            }
            else if(!model.IsNew())
            {
                var oldservice = servicesRepository.Get(model.Id);
                newService.Image = oldservice.Image;
            }

            servicesRepository.CreateOrUpdate(newService);

            return RedirectToAction("ListAdmin");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            servicesRepository.Delete(id);

            return RedirectToAction("ListAdmin");
        }

        private void FillListViewModel(int pageNum)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            var services = servicesRepository.GetBy(skip, settings.ItemsPerPage + 1).Select(s => mapper.Map<ServiceViewModel>(s)).ToList();

            ViewData.Model = services.Take(settings.ItemsPerPage);
            ViewBag.HasPrevPage = pageNum > 1;
            ViewBag.HasNextPage = services.Count > settings.ItemsPerPage;
            ViewBag.PreviosPage = pageNum - 1;
            ViewBag.NextPage = pageNum + 1;
        }

        private readonly ServicesRepository servicesRepository;
        private readonly IMappingEngine mapper;
        private readonly ApplicationSettings settings;
    }
}