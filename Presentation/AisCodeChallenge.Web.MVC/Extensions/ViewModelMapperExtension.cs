using AutoMapper;
using System.Collections.Generic;

namespace AisCodeChallenge.Web.MVC.Extensions
{

    /// <summary>
    /// Extension to mapp different object with AutoMapper .
    /// </summary>
    public static class ViewModelMapperExtension
    {
        public static List<TViewModel> ToViewModelList<TModel, TViewModel>(this List<TModel> model)
        {
            return Mapper.Map<List<TViewModel>>(model);
        }

        public static TDestination ToViewModel<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}