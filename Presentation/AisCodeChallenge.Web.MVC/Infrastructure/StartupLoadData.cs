using AisCodeChallenge.Common;
using AisCodeChallenge.Common.Cache;
using AisCodeChallenge.Common.Model;
using AisCodeChallenge.Services.File;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AisCodeChallenge.Web.MVC.Infrastructure
{
    public class StartupLoadData
    {
        /// <summary>
        /// Clean current direcory data
        /// </summary>
        public  void CleanCurrentDirecoryData()
        {
            var fileServices = DependencyResolver.Current.GetService<IFileServices>();
            fileServices.CleanCurrentDirecoryData();
        }


        /// <summary>
        /// Load Last Activity
        /// </summary>
        public async Task GetLatesActivity()
        {
            try
            {
                var fileServices = DependencyResolver.Current.GetService<IFileServices>();
                var memoryCache = DependencyResolver.Current.GetService<IMemoryCache>();
                var textFilePath = FilesData.MapPathFolder(FilesData.LastFilesUrls) + FilesData.TextFile;
                FilesData.CreateDirectoryIfNotExists(FilesData.LastFiles);
                var pathLastFiles = FilesData.MapPathFolder(FilesData.LastFiles);
                var downloadModel = new DownloadModel{
                    PathToDownload = pathLastFiles,
                };
                FilesData.GetFileTextWithData(textFilePath).ForEach(x => downloadModel.DataUrl.Add(new DataUrl { AbsoluteUri = x }));
                await fileServices.DownloadFiles(downloadModel);
                memoryCache.Set(string.Format(Constants.GetLastActivityListOfFilesCacheKey), fileServices.GetLastActivityListOfFiles(pathLastFiles));
            }
            catch(Exception ex)  {
                //Log Exception
            }
        }
    }
}