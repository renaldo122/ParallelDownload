using AisCodeChallenge.Common.Message;
using AisCodeChallenge.Common.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AisCodeChallenge.Services.File
{
    public interface IFileServices
    {
        /// <summary>
        /// Get list of files
        /// </summary>
        /// <returns></returns>
         IEnumerable<Uri> GetListOfFiles();



        /// <summary>
        ///  /// Get last 10 files stored locally
        /// </summary>
        /// <returns></returns>
        IEnumerable<FilesModel> GetLastActivityListOfFiles(string path);


        /// <summary>
        /// Download Files
        /// </summary>
        /// <param name="downloadData"></param>
        /// <returns></returns>
        Task<IResponseMessage> DownloadFiles(DownloadModel downloadData);


        /// <summary>
        /// CancelDownload
        /// </summary>
        /// <returns></returns>
        IResponseMessage CancelDownload();



        /// <summary>
        /// Clean Curren directory Data
        /// </summary>
        IResponseMessage CleanCurrentDirecoryData();
    }
}
