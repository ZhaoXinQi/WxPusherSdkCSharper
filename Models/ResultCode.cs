using System;
using System.Collections.Generic;
using System.Text;

namespace WxPusherSdkCSharper.Models
{
    public class ResultCode
    {
        /// <summary>
        /// 0 为 false 1 为 true
        /// </summary>
        public ResCode ResCode { get; set; }
        public dynamic ResContent { get; set; }
    }

    public enum ResCode
    {
        错误, 正确
    }
}
