using System.Web;

namespace HappyStation.Web.ControllerServices
{
    public interface IFileUploadService
    {
        string SaveImage(HttpPostedFileBase fileToSave, HttpContextBase context);
        bool DeleteFile(string fileName);
    }
}