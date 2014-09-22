﻿using System.Web;
using System.Web.Mvc;

using HappyStation.Web.Services;

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

        private readonly FileUploadService uploadService;
    }
}