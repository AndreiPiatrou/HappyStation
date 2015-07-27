using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Constants;
using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.ControllerServices;
using HappyStation.Web.Extensions;
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

        [HttpGet]
        public ActionResult HotestServices(int count = 4)
        {
            ViewData.Model = servicesRepository.GetHottest(count).Select(s => mapper.Map<ServiceViewModel>(s));

            return View();
        }

        [Route("service/{id}")]
        public ActionResult Service(string id)
        {
            var service = servicesRepository.Get(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            var mappedModel = mapper.Map<ServiceViewModel>(service);
            ViewData.Model = mappedModel;
            // ViewBag.ShareImageSrc = mappedModel.Description.TryFindFirstImage();

            return View();
        }

        [HttpGet, Route("services/{pageNum=1}")]
        public ActionResult List(int pageNum = 1)
        {
            FillListViewModel(pageNum);

            return View();
        }

        [HttpGet, Authorize, Route("services/admin/{pagenum=1}")]
        public ActionResult ListAdmin(int pageNum = 1)
        {
            FillListViewModel(pageNum);

            return View();
        }

        [Authorize, Route("service/{id=0}/edit")]
        public ActionResult Edit(int id = 0)
        {
            ServiceViewModel model;
            if (id < 1)
            {
                model = new ServiceViewModel();
            }
            else
            {
                var domainEntity = servicesRepository.Get(id);
                if (domainEntity == null)
                {
                    return HttpNotFound();
                }

                model = mapper.Map<ServiceViewModel>(domainEntity);
            }

            return View(model);
        }

        [HttpPost, Authorize, Route("service/save")]
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
            else if (!model.IsNew())
            {
                var oldservice = servicesRepository.Get(model.Id);
                newService.Image = oldservice.Image;
            }

            servicesRepository.CreateOrUpdate(newService);

            return RedirectToAction("ListAdmin");
        }

        [Authorize, Route("service/{id}/delete")]
        public ActionResult Delete(int id)
        {
            servicesRepository.Delete(id);

            return RedirectToAction("ListAdmin");
        }

        [HttpGet]
        public ActionResult PreviewList(int count = Numbers.MaxGetCount)
        {
            ViewData.Model = servicesRepository.GetBy(0, count).Select(s => mapper.Map<ServiceViewModel>(s));

            return View();
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