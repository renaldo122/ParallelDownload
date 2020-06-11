using Microsoft.Extensions.Configuration;
using System.IO;

namespace AisCodeChallenge.Tests.Helper
{
    public class ConfigurationHelper
    {

        #region Fields

        public static string LastFilesPath => ConfigurationRoot[nameof(LastFilesPath)];
        public static string NumberOfFiles => ConfigurationRoot[nameof(NumberOfFiles)];
        public static string PathToDownload => ConfigurationRoot[nameof(PathToDownload)];
        public static string degreeOfParallelism => ConfigurationRoot[nameof(degreeOfParallelism)];

        #endregion


        #region Interface

        public static IConfigurationRoot ConfigurationRoot { get; private set; }

        #endregion


        #region Methods
        /// <summary>
        /// Build Configuration
        /// Get json file configuration 
        /// </summary>
        public static void BuildConfiguration()
        {
            if (ConfigurationRoot != null)
            {
                return;
            }
            var folder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName
                + "\\" + System.Configuration.ConfigurationManager.AppSettings["ConfigurationFolder"];

            IConfigurationBuilder builder = new ConfigurationBuilder().
                SetBasePath(folder).
                AddJsonFile("config.json", false).AddEnvironmentVariables();

            ConfigurationRoot = builder.Build();
        }


        #endregion

    }
}
