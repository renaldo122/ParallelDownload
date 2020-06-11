using AisCodeChallenge.Common.Cache;
using AisCodeChallenge.Common;
using AisCodeChallenge.Common.Message;
using AisCodeChallenge.Common.Model;
using AisCodeChallenge.Services.File;
using AisCodeChallenge.Web.MVC.Extensions;
using AisCodeChallenge.Web.MVC.Factories.File;
using AisCodeChallenge.Web.MVC.ViewModels.File;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;

namespace AisCodeChallenge.Web.MVC.Controllers
{
    public class FileController : Controller
    {
        #region Fields

        private readonly IFileServices _fileServices;
        private readonly IFileModelFactory _fileModelFactory;
        private readonly IMemoryCache _memoryCache;

        #endregion

        #region Constructors

        public FileController(IFileServices fileServices,
            IFileModelFactory fileModelFactory,
             IMemoryCache memoryCache)
        {
            _fileServices = fileServices;
            _fileModelFactory = fileModelFactory;
            _memoryCache = memoryCache;
        }

        #endregion

        #region ActionResult

        public ActionResult Index()
        {
            var model = GetFileViewModel(false);
            return View("Index", model);
        }
        public ActionResult RefreshFileList()
        {
            var model = GetFileViewModel(true);
            return PartialView("FileList", model);
        }


        [HttpGet()]
        public async Task<ActionResult> DownloadFiles(string downloadData, int degreeOfParallelism)
        {
            ResponseMessage response = new ResponseMessage
            {
                Success = true,
                Message = "Download Cancelled!"
            };

            if (!string.IsNullOrEmpty(downloadData))
            {
                var fileData = new DownloadModel
                {
                    DataUrl = JsonConvert.DeserializeObject<List<DataUrl>>(downloadData),
                    PathToDownload = FilesData.MapPathFolder(FilesData.DownloadFiles),
                    degreeOfParallelism = degreeOfParallelism,
                };
                FilesData.CreateDirectoryIfNotExists(FilesData.DownloadFiles);
                if (fileData.DataUrl.Any())
                {
                    response = (ResponseMessage)await _fileServices.DownloadFiles(fileData);
                }
            }

            return Json(new { data = JsonConvertExtensions.GetJsonConvert(response) }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public FileResult DownloadFileToUser(string fileName)
        {
            string strTarget = FilesData.GetCombinePath(FilesData.DownloadFiles, fileName);
            return File(strTarget, Response.ContentType, fileName);
        }


        [HttpGet()]
        public ActionResult CancelDownload()
        {
            var response = _fileServices.CancelDownload();
            return Json(new { data = JsonConvertExtensions.GetJsonConvert(response) }, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region Methods

        private FileViewModel GetFileViewModel(bool isRefresh)
        {
            var model = new FileViewModel();
            var dataFiles = _fileServices.GetListOfFiles().ToList();
            if (!isRefresh)
            {
                var path = FilesData.MapPathFolder(FilesData.LastFiles);
                var lastActivity = _memoryCache.Get(string.Format(Constants.GetLastActivityListOfFilesCacheKey), () => _fileServices.GetLastActivityListOfFiles(path).ToList());
                _fileModelFactory.PrepareLastFileViewModel(lastActivity, model);
            }
            _fileModelFactory.PrepareFileViewModel(dataFiles, model);
            RefreshLastActivityList(dataFiles);
            return model;
        }

        private bool RefreshLastActivityList(List<Uri> urls)
        {
            try
            {
                FilesData.CreateDirectoryIfNotExists(FilesData.LastFilesUrls);
                var path = FilesData.GetDirectoryPath(FilesData.LastFilesUrls, FilesData.TextFile);
                FilesData.SetFileTextWithData(path, urls);
                return true;
            }
            catch (Exception ex)  {
                //Log Exception
                return false;
            }

        }
        #endregion
    }
}