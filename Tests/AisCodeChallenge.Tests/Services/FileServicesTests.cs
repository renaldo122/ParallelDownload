using AisCodeChallenge.Tests.Base;
using AisCodeChallenge.Tests.Extensions;
using AisCodeChallenge.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AisCodeChallenge.Tests.Services
{
    //Check configuration at config.json for running tests 
    [TestClass]
    public class FileServicesTests : BaseTestInitalizer
    {

        [TestInitialize]
        public override void SetupTest()
        {
            ConfigurationHelper.BuildConfiguration();
        }


        [TestMethod]
        public void GetListOfFilesTest()
        {
            var listOfFiles = _fileServices.GetListOfFiles().ToList();
            listOfFiles.Count().ShouldEqual(Convert.ToInt32(ConfigurationHelper.NumberOfFiles));
        }


        [TestMethod]
        public void GetLastActivityListOfFilesTest()
        {
            var lastActivity = _fileServices.GetLastActivityListOfFiles(ConfigurationHelper.LastFilesPath);
            lastActivity.Count().ShouldEqual(Convert.ToInt32(ConfigurationHelper.NumberOfFiles));
        }


        [TestMethod]
        public async Task DownloadFilesTest()
        {
            var downloadModel = GetDownloadModelTest();
            var response = await _fileServices.DownloadFiles(downloadModel);
            response.Success.ShouldEqual(true);
        }


        [TestMethod]
        public void CancelDownloadTest()
        {
            _mockfileServices.Setup(x => x.CancelDownload()).Returns(SetCancelledOnThread());
            var response = _mockfileServices.Object.CancelDownload();
            response.Success.ShouldEqual(true);
        }
    }
}
