using System.Web;
using System.Web.Mvc;

using HappyStation.Web.ControllerServices;

namespace HappyStation.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase(FileUploadService uploadService)
        {
            this.uploadService = uploadService;
        }

        protected string UploadFile(HttpPostedFileBase fileToSave)
        {
            return uploadService.SaveImage(fileToSave, HttpContext);
        }

        protected bool DeleteFile(string fileName)
        {
            return uploadService.DeleteFile(Request.MapPath("~" + fileName));
        }

        private readonly FileUploadService uploadService;
    }
}