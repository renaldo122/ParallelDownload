using System;

namespace AisCodeChallenge.Common.Extensions
{

    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception ex,string additionmessage="")
        {
            var message = "";
            var exceptionMessage = (ex.InnerException == null) ? ex.Message : ex.Message + "  " + ex.InnerException.GetFullMessage();
            if (!string.IsNullOrEmpty(additionmessage))
                message = additionmessage + " " + Environment.NewLine + exceptionMessage;
            else
                message = exceptionMessage;

            return message;
        }
    }
}
