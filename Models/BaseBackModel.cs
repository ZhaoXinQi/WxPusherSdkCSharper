using System;
using System.Collections.Generic;
using System.Text;

namespace WxPusherSdkCSharper.Models
{
    public class BaseBackModel<T> where T : class 
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual T data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
    }
}
