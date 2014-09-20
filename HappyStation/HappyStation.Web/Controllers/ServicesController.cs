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
    public class ServicesController : Controller
    {
        public ServicesController(ServicesRepository servicesRepository, IMappingEngine mapper, ApplicaitonSettings settings)
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
            throw new NotImplementedException();
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

        public ActionResult ListAdmin(int pageNum = 1)
        {
            var skip = (pageNum - 1) * settings.ItemsPerPage;
            ViewData.Model = servicesRepository.GetBy(skip, settings.ItemsPerPage).Select(s => mapper.Map<ServiceViewModel>(s));

            return View();
        }

        private readonly ServicesRepository servicesRepository;
        private readonly IMappingEngine mapper;
        private readonly ApplicaitonSettings settings;
    }
}