using System.Collections.Generic;

namespace AisCodeChallenge.Common.Model
{
    public class DownloadModel
    {
        public DownloadModel(){
            DataUrl = new List<DataUrl>();
        }

        public string PathToDownload { get; set; }
        public int degreeOfParallelism { get; set; }
        public List<DataUrl> DataUrl { get; set; }
    }

    public class DataUrl
    {
        public string AbsoluteUri { get; set; }
    }
}
