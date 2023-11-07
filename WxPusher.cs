using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WxPusherSdkCSharper.Models;

namespace WxPusherSdkCSharper
{
    public class WxPusher
    {
        private string _AppToken { get; set; }
        private string _BaseUrl { get; set; } = "https://wxpusher.zjiecode.com/api";
        private string _SendMsgApi { get; set; } = "/send/message";
        private string _QueryUserListApi { get; set; } = "/fun/wxuser/v2";
        private string _QrcodeApi { get; set; } = "/fun/wxuser/v2";
        private string _CheckMsgStateApi { get; set; } = "/send/query/status?sendRecordId={0}";
        private string _DeleteMsgApi { get; set; } = "/send/message?messageContentId={0}&appToken={1}";
        private string _DeleteUserApi { get; set; } = "/fun/remove?appToken={0}&id={1}";
        private string _RejectUserApi { get; set; } = "/fun/reject";
        private string _QueryQrCodeUidApi { get; set; } = "/fun/scan-qrcode-uid?code={0)";
        private const int _SdkErrCode = 9999;

        public WxPusher(string token, string baseUrl = null, string sendMsgApi = null,string getUserListApi=null,
            string checkMsgState=null,string deleteMsgApi=null,string deleteUserApi=null)
        {
            this._AppToken = token;
            if (!string.IsNullOrEmpty(baseUrl)) this._BaseUrl = baseUrl;
            if (!string.IsNullOrEmpty(sendMsgApi)) this._SendMsgApi = sendMsgApi;
            if (!string.IsNullOrEmpty(getUserListApi)) this._QueryUserListApi = getUserListApi;
            if (!string.IsNullOrEmpty(checkMsgState)) this._CheckMsgStateApi = checkMsgState;
            if (!string.IsNullOrEmpty(deleteMsgApi)) this._DeleteMsgApi = deleteMsgApi;
            if (!string.IsNullOrEmpty(deleteUserApi)) this._DeleteUserApi = deleteUserApi;
        }


        /// <summary>
        /// 发送消息接口
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="uids"></param>
        /// <param name="summary"></param>
        /// <param name="contenType"></param>
        /// <param name="topicIds"></param>
        /// <param name="url"></param>
        /// <param name="verifyPay"></param>
        /// <returns></returns>
        public async Task<SendMsgBackModel> SendMsg(string Content, List<string> uids, string summary = null, ContenType contenType = ContenType.Text, List<string> topicIds = null, string url = null, bool verifyPay = false)
        {
            try
            {
                var sendMsgBackModel = new SendMsgBackModel();
                var sendMsgModel = new SendMsgModel()
                {
                    appToken = this._AppToken,
                    content = Content,
                    summary = summary,
                    contentType = contenType,
                    topicIds = topicIds,
                    uids = uids,
                    url = url,
                    verifyPay = verifyPay
                };

                using (HttpClient hc = new HttpClient())
                {
                    HttpContent hcContent = new StringContent(JsonConvert.SerializeObject(sendMsgModel));
                    hcContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var resp = await hc.PostAsync(this._BaseUrl + this._SendMsgApi, hcContent);
                    sendMsgBackModel = JsonConvert.DeserializeObject<SendMsgBackModel>(resp.Content.ReadAsStringAsync().Result);
                }
                return sendMsgBackModel;
            }
            catch (Exception e)
            {
                return new SendMsgBackModel()
                {
                    code = _SdkErrCode,
                    msg = $"函数发生异常,异常堆栈：{e.Source}",
                    success = false,

                };
            }
        }

        /// <summary>
        /// 查询用户列表V2
        /// </summary>
        /// <param name="page">请求数据的页码</param>
        /// <param name="pageSize">分页大小，不能超过100</param>
        /// <param name="uid">可选，如果不传就是查询所有用户，传uid就是查某个用户的信息。</param>
        /// <param name="isBlock">查询拉黑用户，可选，不传查询所有用户，true查询拉黑用户，false查询没有拉黑的用户</param>
        /// <param name="type">关注的类型，可选，不传查询所有用户，0是应用，1是主题。 返回数据</param>
        public async Task<WxUserBackModel> GetUserList(int page,int pageSize,string uid =null, string isBlock=null,string type = null)
        {
            try
            {
                var wxUserBackModel = new WxUserBackModel();
                using (HttpClient hc = new HttpClient())
                {
                    var url = string.Format(this._BaseUrl + this._QueryUserListApi + $"?appToken={this._AppToken}&page={page}&pageSize={pageSize}&uid={uid}&isBlock={isBlock}&type={type}");
                    var resp = await hc.GetAsync(url);
                    var respStr = resp.Content.ReadAsStringAsync();
                    wxUserBackModel = JsonConvert.DeserializeObject<WxUserBackModel>(respStr.Result);
                }
                return wxUserBackModel;
            }
            catch ( Exception  e)
            {
                return new WxUserBackModel()
                {
                    code = _SdkErrCode,
                    msg = $"函数发生异常,异常堆栈：{e.Source}",
                    success = false,

                };
            }
        }

        /// <summary>
        /// 查询消息
        /// </summary>
        /// <param name="sendRedcordId">
        /// 发送消息接口返回的发送id，对应给一个uid或者topic的发送id
        /// </param>
        /// <returns></returns>
        public async Task<BaseBackModel<string>> CheckSendMsgState(string sendRedcordId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    var url = this._BaseUrl + string.Format(this._CheckMsgStateApi, sendRedcordId);
                    var resp = await hc.GetAsync(url);
                    var respContent = resp.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<BaseBackModel<string>>(respContent);
                }
            }
            catch (Exception e)
            {
                return new BaseBackModel<string>()
                {
                    code = _SdkErrCode,
                    msg = $"函数发生异常,异常堆栈：{e.Source}",
                    success = false,

                };
            }
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="messageContentId">
        /// messageContentId 发送接口返回的消息内容id，调用一次接口生成一个，如果是发送给多个用户，多个用户共享一个messageContentId，通过messageContentId可以删除内容，删除后本次发送的所有用户都无法再查看本条消息
        /// </param>
        /// <returns></returns>
        public async Task<BaseBackModel<string>> DeleteMsg(string messageContentId)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    var url = this._BaseUrl + string.Format(this._DeleteMsgApi, messageContentId, this._AppToken);
                    var resp = await hc.DeleteAsync(url);
                    var respContent = resp.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<BaseBackModel<string>>(respContent);
                }
            }
            catch (Exception e)
            {
                return new BaseBackModel<string>()
                {
                    code = _SdkErrCode,
                    msg = $"函数发生异常,异常堆栈：{e.Source}",
                    success = false,

                };
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户Id,可以用用户列表接口获取，不要与Uid混淆</param>
        /// <returns></returns>
        public async Task<BaseBackModel<string>> DeleteUser(string id)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    var url = this._BaseUrl + string.Format(this._DeleteUserApi, this._AppToken, id);
                    var resp = await hc.DeleteAsync(url);
                    var respContent = resp.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<BaseBackModel<string>>(respContent);
                }
            }
            catch  (Exception e)
            {
                return new BaseBackModel<string>()
                {
                    code= 1100,
                    msg = $"函数发生异常,异常堆栈：{e.Source}",
                    success = false,
                    
                };
            }
           
        }

        /// <summary>
        /// 拉黑用户
        /// </summary>
        /// <param name="id">用户Id,可以用用户列表接口获取，不要与Uid混淆</param>
        /// <param name="reject">是否拉黑，true表示拉黑，false表示取消拉黑。默认false</param>
        /// <returns></returns>
        public async Task<BaseBackModel<string>> RejectUser(string id,bool reject = false)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    var putModel = new
                    {
                        appToken = this._AppToken,
                        id = id,
                        reject = reject
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(putModel)); 
                    var resp = await hc.PutAsync(this._RejectUserApi, content);
                    var respContent = resp.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<BaseBackModel<string>>(respContent);
                }
            }
            catch (Exception e)
            {
                return new BaseBackModel<string>()
                {
                    code = _SdkErrCode,
                    msg = $"函数发生异常,异常堆栈：{e.Source}",
                    success = false,

                };
            }
        }


        /// <summary>
        /// 创建参数二维码
        /// </summary>
        /// <param name="extra">必填，二维码携带的参数，最长64位</param>
        /// <param name="validTime">可选，二维码的有效期，默认30分钟，最长30天，单位是秒</param>
        /// <returns></returns>
        public async Task<QrCodeBackModel> QrCode(string extra,string validTime)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    var putModel = new
                    {
                        appToken = this._AppToken,
                        extra = extra,
                        validTime = validTime
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(putModel));
                    var resp = await hc.PostAsync(this._QrcodeApi, content);
                    var respContent = resp.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<QrCodeBackModel>(respContent);
                }
            }
            catch (Exception e)
            {
                return new QrCodeBackModel()
                {
                    code = 1100,
                    msg = $"函数发生异常,异常堆栈：{e.Source}",
                    success = false,

                };
            }

        }

        /// <summary>
        /// 查询扫码用户UID
        /// </summary>
        /// <param name="sendRedcordId">
        /// 发送消息接口返回的发送id，对应给一个uid或者topic的发送id
        /// </param>
        /// <returns></returns>
        public async Task<BaseBackModel<string>> QueryQrcodeUid(string code)
        {
            try
            {
                using (HttpClient hc = new HttpClient())
                {
                    var url = this._BaseUrl + string.Format(this._QueryQrCodeUidApi, code);
                    var resp = await hc.GetAsync(url);
                    var respContent = resp.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<BaseBackModel<string>>(respContent);
                }
            }
            catch (Exception e)
            {
                return new BaseBackModel<string>()
                {
                    code = _SdkErrCode,
                    msg = $"函数发生异常,异常堆栈：{e.Source}",
                    success = false,

                };
            }
        }

    }
}
