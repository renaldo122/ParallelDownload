using System;
using AisCodeChallenge.Common;
using AisCodeChallenge.Common.Model;
using AisCodeChallenge.Web.MVC.ViewModels.File;
using AutoMapper;

namespace AisCodeChallenge.Web.MVC
{
    public partial class AutoMapperConfig
    {

        /// <summary>
        /// Register Configuration for AutoMapper and mapping properties
        /// </summary>
        public static void RegisterConfiguration()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Uri, FileDataViewModel>()
               .ForMember(m => m.FileName, d => d.MapFrom(src => FilesData.GetFileName(src.LocalPath)))
               .ForMember(m => m.Extension, d => d.MapFrom(src => FilesData.GetFileExtension(src.LocalPath)));


                cfg.CreateMap<FilesModel, FileDataViewModel>()
                 .ForMember(m => m.AbsoluteUri, d => d.Ignore())
                 .ForMember(m => m.Authority, d => d.Ignore())
                 .ForMember(m => m.LocalPath, d => d.Ignore());
            });

            Mapper.Configuration.AssertConfigurationIsValid();

        }


    }
}
