using AisCodeChallenge.Web.MVC.ViewModels.File;
using System.Web;
using System.Web.Mvc;
using AisCodeChallenge.Common;
using System;

namespace AisCodeChallenge.Web.MVC.Extensions
{
    /// <summary>
    /// HtmlHelper Extensions
    /// </summary>
    public static class HtmlHelperExtensions
    {

        public static IHtmlString GetPreviewColumn(this HtmlHelper helper, FileDataViewModel viewModel)
        {
            var html = "";
            var outerDiv = new TagBuilder("img");
            if (Constants.imageString.Contains(viewModel.Extension)) {
                outerDiv.AddCssClass("img-responsive img-gridCss");
                outerDiv.MergeAttribute("src", viewModel.AbsoluteUri);
                outerDiv.MergeAttribute("width", Constants.ImageWidth);
                outerDiv.MergeAttribute("height", Constants.ImageHeight);
            }
            else if (Constants.TextString.Contains(viewModel.Extension))  {
                outerDiv = new TagBuilder("p");
                outerDiv.AddCssClass("CssFileTableTd");
                var text = FilesData.GetTextFromUrl(viewModel.AbsoluteUri);
                var textToShow = text.Substring(0, Math.Min(text.Length, 15)) +"...";
                 outerDiv.SetInnerText(text);
            }
            else {
                outerDiv.SetInnerText("Invalid format to preview");
            }
            html = outerDiv.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(html);
        }
    }
}