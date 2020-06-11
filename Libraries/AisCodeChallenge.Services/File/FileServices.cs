using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AisCodeChallenge.Common;
using AisCodeChallenge.Common.Extensions;
using AisCodeChallenge.Common.Message;
using AisCodeChallenge.Common.Model;
using AisUriProviderApi;

namespace AisCodeChallenge.Services.File
{
    public class FileServices : IFileServices
    {
        #region Fields

        private static CancellationTokenSource _cancelToken;
        #endregion

        #region Methods

        /// <inheritdoc />
        public IEnumerable<Uri> GetListOfFiles()
        {
            return new AisUriProvider().Get();
        }

        /// <inheritdoc />
        public IEnumerable<FilesModel> GetLastActivityListOfFiles(string path)
        {
            var data = new List<FilesModel>();
            try
            {
                var allFiles = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly).
                ToList().OrderByDescending(d => new FileInfo(d).LastWriteTimeUtc);
                foreach (var file in allFiles)
                {
                    FileInfo infoFile = new FileInfo(file);
                    var itemFile = new FilesModel {
                        FileName = infoFile.Name,
                        Extension = infoFile.Extension
                    };
                    data.Add(itemFile);
                }
            }
            catch (Exception ex)  {
                //Log error
            }
            return data;
        }


        /// <inheritdoc />
        public async Task<IResponseMessage> DownloadFiles(DownloadModel downloadData)
        {
            ResponseMessage response = new ResponseMessage
            {
                Success = true,
                Message = "Download completed!"
            };
            try
            {
                string FileName = "";

                _cancelToken = new CancellationTokenSource();
                var po = new ParallelOptions();
                po.CancellationToken = _cancelToken.Token;
                po.MaxDegreeOfParallelism = downloadData.degreeOfParallelism <= 0 ? Environment.ProcessorCount : downloadData.degreeOfParallelism;
                try
                {
                  await Task.Factory.StartNew(() => Parallel.ForEach(downloadData.DataUrl, po, (itemData, loopstate) =>
                    {
                        DownloadFile(itemData, downloadData.PathToDownload);
                        FileName += Path.GetFileName(itemData.AbsoluteUri) + ";";
                        po.CancellationToken.ThrowIfCancellationRequested();
                    }), TaskCreationOptions.LongRunning);

                }
                catch (OperationCanceledException e) {
                    response.Success = false;
                    response.Message = e.GetFullMessage(); 
                }
                response.CustomAction.Add("FileName", FileName);
                return response;
            }
            catch (Exception ex)  {
                response.Success = false;
                response.Message = ex.GetFullMessage("Failed to download file!");
                return response;
            }
            finally
            {
                _cancelToken.Dispose();
            }
        }

        private void DownloadFile(DataUrl dataUrl, string pathToDownload)
        {
            try
            {
                if (string.IsNullOrEmpty(dataUrl.AbsoluteUri)) return;
                var pathEnds = pathToDownload.EndsWith("\\") ? "" : "\\";
                string fullPath = pathToDownload + pathEnds + Path.GetFileName(dataUrl.AbsoluteUri);

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(dataUrl.AbsoluteUri, fullPath);
                }

            }catch(Exception ex) {
                //  //Log Exception
            }

        }

        /// <inheritdoc />
        public IResponseMessage CancelDownload()
        {
            ResponseMessage response = new ResponseMessage{
                Success = false,
                Message = "Can't cancel download"
            };
            try
            {
                if (_cancelToken != null)
                {
                    _cancelToken.Cancel();
                    _cancelToken.Dispose();
                    response.Success = true;
                    response.Message = "Download Cancelled!";
                }
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.GetFullMessage("Failed to cancel download");
                return response;
            }
        }


        /// <inheritdoc />
        public IResponseMessage CleanCurrentDirecoryData()
        {
            ResponseMessage response = new ResponseMessage {
                Success = true,
                Message = "Direcory deleted sucessfully"
            };
            try    {
                FilesData.DeleteDirectoryOrContent(FilesData.MapPathFolder(FilesData.DownloadFiles), true);
                FilesData.DeleteDirectoryOrContent(FilesData.MapPathFolder(FilesData.LastFiles), true);
                return response;
            }catch (Exception ex) {
                response.Success = false;
                response.Message = ex.GetFullMessage();
                return response;
            }
        }
        #endregion
    }
}
