using System.Collections.Generic;

namespace Multi_language.ApiHelper.Response
{
    public class ErrorStateResponse
    {
        public string Message { get; set; }
        public IDictionary<string, string[]> ModelState { get; set; }
    }
}