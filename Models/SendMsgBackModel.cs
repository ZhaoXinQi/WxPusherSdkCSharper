using System;
using System.Collections.Generic;
using System.Text;

namespace WxPusherSdkCSharper.Models
{
    public class SendMsgBackModel:BaseBackModel<List<Datum>>
    {

        /// <summary>
        /// 每个uid/topicid的发送状态，和发送的时候，一一对应，是一个数组，可能有多个
        /// </summary>
        public override List<Datum> data { get; set; }
       
    }

    public class Datum
    {
        /// <summary>
        /// 用户uid
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 主题ID
        /// </summary>
        public object topicId { get; set; }

        /// <summary>
        /// 废弃⚠️，请不要再使用，后续会删除这个字段
        /// </summary>
        public int messageId { get; set; }

        /// <summary>
        /// 消息内容id，调用一次接口，生成一个，你可以通过此id调用删除消息接口，删除消息。本次发送的所有用户共享此消息内容。
        /// </summary>
        public int messageContentId { get; set; }

        /// <summary>
        /// 消息发送id，每个uid用户或者topicId生成一个，可以通过这个id查询对某个用户的发送状态
        /// </summary>
        public int sendRecordId { get; set; }

        /// <summary>
        /// 1000表示发送成功
        /// </summary>
        public int code { get; set; }
        public string status { get; set; }
    }




}
