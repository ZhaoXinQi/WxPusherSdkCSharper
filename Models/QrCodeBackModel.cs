using System;
using System.Collections.Generic;
using System.Text;

namespace WxPusherSdkCSharper.Models
{
    public class QrCodeBackModel :BaseBackModel<QrCodeModel>
    {
        public override QrCodeModel data { get; set; }
    }

    public class QrCodeModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int expires { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shortUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
    }
}
