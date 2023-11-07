using System;
using System.Collections.Generic;
using System.Text;

namespace WxPusherSdkCSharper.Models
{
    public class WxUserBackModel:BaseBackModel<Data>
    {
        /// <summary>
        /// 
        /// </summary>
        public override Data data { get; set; }
    }
    public class RecordsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int wxUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string headImg { get; set; }
        /// <summary>
        /// 樱桃味
        /// </summary>
        public string nickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int payEndTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int remove { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string target { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int appOrTopicId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int key { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RecordsItem> records { get; set; }
    }

 



}