using AisCodeChallenge.Common.Model;
using AisCodeChallenge.Web.MVC.ViewModels.File;
using System;
using System.Collections.Generic;

namespace AisCodeChallenge.Web.MVC.Factories.File
{
    public interface IFileModelFactory
    {

        /// <summary>
        /// Prepare File ViewModel
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        void PrepareFileViewModel(IEnumerable<Uri> data, FileViewModel model);



        /// <summary>
        /// Prepare File ViewModel
        /// </summary>
        /// <param name="data"></param>
        /// <param name="model"></param>
        void PrepareLastFileViewModel(IEnumerable<FilesModel> data, FileViewModel model);
    }
}