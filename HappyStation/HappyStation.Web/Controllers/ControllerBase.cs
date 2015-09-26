using System.Web;
using System.Web.Mvc;

using HappyStation.Web.ControllerServices;

namespace HappyStation.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase(IFileUploadService uploadService)
        {
            this.uploadService = uploadService;
        }

        protected string UploadFile(HttpPostedFileBase fileToSave)
        {
            return uploadService.SaveImage(fileToSave, HttpContext);
        }

        protected bool DeleteFile(string fileName)
        {
            fileName = fileName.StartsWith("http") ? fileName : Request.MapPath("~" + fileName);
            return uploadService.DeleteFile(fileName);
        }

        private readonly IFileUploadService uploadService;
    }
}