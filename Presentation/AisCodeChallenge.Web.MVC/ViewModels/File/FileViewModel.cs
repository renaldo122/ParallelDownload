using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AisCodeChallenge.Web.MVC.ViewModels.File
{
    public class FileViewModel
    {
        public FileViewModel() {
            FileData = new List<FileDataViewModel>();
            LastActivityFiles = new List<FileDataViewModel>();
        }
        public List<FileDataViewModel> FileData { get; set; }
        public List<FileDataViewModel> LastActivityFiles { get; set; }
    }

    public class FileDataViewModel
    {
        public string AbsoluteUri { get; set; }
        public string Authority { get; set; }
        public string LocalPath { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }

    }
}