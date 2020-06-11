using AisCodeChallenge.Common.Model;
using AisCodeChallenge.Web.MVC.Extensions;
using AisCodeChallenge.Web.MVC.ViewModels.File;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AisCodeChallenge.Web.MVC.Factories.File
{
    public class FileModelFactory : IFileModelFactory
    {

        /// <inheritdoc />
        public virtual void PrepareFileViewModel(IEnumerable<Uri> data,FileViewModel model)
        {
            model.FileData.AddRange(data.ToList().ToViewModelList<Uri, FileDataViewModel>());
        }

        /// <inheritdoc />
        public virtual void PrepareLastFileViewModel(IEnumerable<FilesModel> data, FileViewModel model)
        {
            model.LastActivityFiles.AddRange(data.ToList().ToViewModelList<FilesModel, FileDataViewModel>());
        }
    }
}