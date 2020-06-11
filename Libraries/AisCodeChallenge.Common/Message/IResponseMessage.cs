using System.Collections.Generic;

namespace AisCodeChallenge.Common.Message
{
    /// <summary>
    /// Response Message 
    /// </summary>
    public interface IResponseMessage
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { set; }
        Dictionary<object, object> CustomAction { get; set; }
    }
}
