using AisCodeChallenge.Common.Message;
using AisCodeChallenge.Common.Model;
using AisCodeChallenge.Services.File;
using AisCodeChallenge.Tests.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace AisCodeChallenge.Tests.Base
{
    [TestClass]
    public abstract class BaseTestInitalizer
    {

        #region Services
        public static IConfigurationRoot ConfigurationRoot { get; private set; }
        public Mock<IFileServices> _mockfileServices;
        public IFileServices _fileServices;

        #endregion


        #region Initialize
        [TestInitialize]
        public virtual void Initialize()
        {
            _mockfileServices = new Mock<IFileServices>();
            _fileServices = new FileServices();
        }

        #endregion


        #region Cleanup

        [TestCleanup]
        public void Cleanup()
        {
        }

        #endregion


        #region Methods

        /// <summary>
        /// Set Cancelled On Task
        /// </summary>
        /// <returns></returns>
        public static ResponseMessage SetCancelledOnThread()
        {
            ResponseMessage response = new ResponseMessage { Success = false, };
            var cancelToken = new CancellationTokenSource();
            if (cancelToken != null)
            {
                cancelToken.Cancel();
                cancelToken.Dispose();
                response.Success = true;
            }
            return response;
        }

        /// <summary>
        /// Get Download Model
        /// </summary>
        /// <returns></returns>
        public DownloadModel GetDownloadModelTest()
        {
            var downloadModel = new DownloadModel();
            var dataFiles = _fileServices.GetListOfFiles();
            foreach (var item in dataFiles)
            {
                downloadModel.DataUrl.Add(new DataUrl { AbsoluteUri = item.AbsoluteUri });
            }
            downloadModel.degreeOfParallelism = Convert.ToInt32(ConfigurationHelper.degreeOfParallelism);
            downloadModel.PathToDownload = ConfigurationHelper.PathToDownload;
            return downloadModel;
        }

        #endregion


        #region SetupTest

        public abstract void SetupTest();

        #endregion

    }
}
