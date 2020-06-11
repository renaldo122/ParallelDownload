using System;
using AisCodeChallenge.Common.Message;
using Newtonsoft.Json;

namespace AisCodeChallenge.Web.MVC.Extensions
{
    /// <summary>
    /// Convert from IResponseMessage to json
    /// </summary>
    public class JsonConvertExtensions
    {

        /// <summary>
        /// Convert Response Message to Json
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetJsonConvert(IResponseMessage response)
        {
            try
            {
                var jsonResponse = JsonConvert.SerializeObject(response, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return jsonResponse;
            }
            catch (Exception)
            {
                var responseError = new ResponseMessage
                {
                    Success = true,
                    Message = "AnErrorOccurred"
                };
                var jsonResponse = JsonConvert.SerializeObject(responseError, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return jsonResponse;
            }
        }
    }
}