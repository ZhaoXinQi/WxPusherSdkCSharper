# WxPusherSdkCSharper
WxPusher (微信推送服务)SDK

## 1. 下载SDK

## 2. 添加到项目中

## 3. 引用该SDK

## 4. 使用

### 4.1 发送消息

``` 	c#
var _Wxpusher = new WxPusher("你的Token");
var res =  _Wxpusher.SendMsg(“发送内容”, new List<string>() { 用户ID });
```

### 4.3 查询状态

``` 	c#
var _Wxpusher = new WxPusher("你的Token");
var res =  _Wxpusher.CheckSendMsgState("发送消息的sendRedcordId");
```

### 4.4 查询用户列表V2

``` 	c#
var _Wxpusher = new WxPusher("你的Token");
var res =  _Wxpusher.GetUserList(page,pageSize);
```

### 4.5 删除消息

``` 	c#
var _Wxpusher = new WxPusher("你的Token");
var res =  _Wxpusher.DeleteMsg(“发送内容messageContentId”);
```

### 4.6 删除用户

``` 	c#
var _Wxpusher = new WxPusher("你的Token");
var res =  _Wxpusher.DeleteUser(“删除的id”);
```

### 4.7 拉黑用户

``` 	c#
var _Wxpusher = new WxPusher("你的Token");
var res =  _Wxpusher.RejectUser(“拉黑的id”);
```

