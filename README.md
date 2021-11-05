# sharp-websocketcp
允许浏览器与远程 TCP 服务器交互,简单实现webdsocket与远程TCP的双向通信

----
#### 项目websocket功能基于websocket-sharp（https://github.com/PingmanTools/websocket-sharp/）

#### 项目命令行功能基于websocket-sharp（https://github.com/PingmanTools/websocket-sharp/）

---

###### 启动说明：程序命令行的方式启动：
##### dotnet sharp-websocketcp.dll bind [socoket服务端ip:端口] [TCP服务端ip:端口]
##### 例如：

```console

dotnet sharp-websocketcp.dll bind 127.0.0.1:12345 127.0.0.1:6666
```
