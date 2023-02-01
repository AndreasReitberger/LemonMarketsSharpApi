using Newtonsoft.Json;
using System;

namespace AndreasReitberger.API.Models.REST.Events
{
    public class LemonMarketsJsonConvertEventArgs : EventArgs
    {
        #region Properties
        public string Message { get; set; }
        public string OriginalString { get; set; }
        public string TargetType { get; set; }
        public Exception Exception { get; set; }
        #endregion

        #region Overrides
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
