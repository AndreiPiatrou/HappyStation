using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Services.Implementations;
using HappyStation.Web.Services;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class HandMadeController : ControllerBase
    {
        public HandMadeController(FileUploadService uploadService, HandMadesService handMadesService, IMappingEngine mapper)
            : base(uploadService)
        {
            Contract.Requires(handMadesService != null);
            Contract.Requires(mapper != null);

            this.handMadesService = handMadesService;
            this.mapper = mapper;
        }

        public ActionResult LastHandMades(int count = 5)
        {
            var handMades = handMadesService.GetBy(0, count).Select(h => mapper.Map<HandMadeViewModel>(h));
            ViewData.Model = handMades;

            return View();
        }

        private readonly HandMadesService handMadesService;
        private readonly IMappingEngine mapper;

        public ActionResult HandMade()
        {
            throw new System.NotImplementedException();
        }
    }
}