using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using HappyStation.Core.Entities;
using HappyStation.Core.Services.Implementations;
using HappyStation.Web.ViewModels;

namespace HappyStation.Web.Controllers
{
    public class PageContentController : Controller
    {
        public PageContentController(PageContentRepository pageContentRepository, IMappingEngine mapper)
        {
            Contract.Requires(pageContentRepository != null);
            Contract.Requires(mapper != null);

            this.pageContentRepository = pageContentRepository;
            this.mapper = mapper;
        }

        [HttpGet, Authorize, Route("pagecontent/admin")]
        public ActionResult ListAdmin()
        {
            return View(pageContentRepository.GetBy(0, 100).Select(p => mapper.Map<PageContentViewModel>(p)));
        }

        [Authorize, Route("pagecontent/{id=0}/edit")]
        public ActionResult Edit(int id)
        {
            PageContentViewModel model;
            if (id < 1)
            {
                model = new PageContentViewModel();
            }
            else
            {
                var domainEntity = pageContentRepository.Get(id);
                if (domainEntity == null)
                {
                    return HttpNotFound();
                }

                model = mapper.Map<PageContentViewModel>(domainEntity);
            }

            return View(model);
        }

        [HttpPost, Authorize, Route("pagecontent/save")]
        public ActionResult Save(PageContentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var newPageContent = mapper.Map<PageContent>(model);

            pageContentRepository.CreateOrUpdate(newPageContent);

            return RedirectToAction("ListAdmin");
        }

        [Authorize, Route("pagecontent/{id}/delete")]
        public ActionResult Delete(int id)
        {
            pageContentRepository.Delete(id);

            return RedirectToAction("ListAdmin");
        }

        private readonly PageContentRepository pageContentRepository;
        private readonly IMappingEngine mapper;

    }
}