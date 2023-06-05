<template>
  <div class="hello">
    <ul>
      <li v-for="(item, index) in remsg" :key="index">
        {{ item }}
      </li>
    </ul>
    <input type="text" placeholder="请输入用户名" v-model="user" />
    <input type="text" placeholder="密码" v-model="pwd" />
    <button @click="login">登录</button>
    <ul>
      <li v-for="(item, index) in onlineUser" :key="index">
        {{ item }}
      </li>
    </ul>
    <input type="text" placeholder="请输入内容" v-model="message" />
    <button @click="handle">发送消息</button>message
  </div>
</template>

<script>
import * as signalR from "@microsoft/signalr";

export default {
  name: "StoreHome",
  data() {
    return {
      user: "",
      pwd: "",
      remsg: ["test", "test3"],
      message: "",
      onlineUser: ["testUser"],
      clientId: "",
      connection: null,
    };
  },
  created() {
    //---登录---
  },
  methods: {
    login() {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7278/chatHub")
        // .withAutomaticReconnect() //断线自动重连
        // .configureReconnectBehavior((reconnectOptions) => {
        //   reconnectOptions.userProvidedAccessToken = "111111"; // 设置自定义的访问令牌
        //   reconnectOptions.retryDelays = [1000, 3000, 5000]; // 设置重新连接的延迟时间
        // })
        .build();
      this.connection.start().then(() => {
        this.connection
          .invoke("Login", this.user, this.pwd)
          .catch(function (err) {
            return console.error(err.toString());
          });
        var chat = this;
        this.connection.on("SendMessageResponse", function (res) {
          if (res && res.status == 0) {
            console.log("SendMessageResponse", res);
            chat.remsg.push(res.message);
            // var li = document.createElement("li");
            // li.textContent = res.message;
            // document.getElementById("messagesList").appendChild(li);
          } else {
            alert(res.message);
          }
        });
        //---消息---
        this.connection.on("LoginResponse", function (res) {
          if (res && res.status == 0) {
            // sessionStorage.setItem("curuser", res.data);
            alert(res.message);
            // chat.getUsers();
          } else {
            alert("登录失败！");
          }
        });
        this.connection.on("GetUsersResponse", (res) => {
          alert(1);
          if (res && res.status == 0) {
            console.log("GetUsersResponse", res);
            this.remsg.push(res.data.userName + "离开了");
            if (this.onlineUser.indexOf(res.data.userName)) {
              this.onlineUser.splice(
                this.onlineUser.indexOf(res.data.userName),
                1
              );
            }
            console.log("onlineUser", res);
            console.log(this.onlineUser);
          }
        });
      });

      //自动重连成功后的处理
      this.connection.onreconnected((connectionId) => {
        alert(connectionId);
      });
      // console.log("logon");
      // setTimeout(() => {

      // }, 1000);
      this.connection.onclose((error) => {
        // 连接断开时的处理
        if (error) {
          alert("连接断开，错误信息:", error);
        } else {
          alert("连接断开");
        }
      });
    },
    handle() {
      this.connection
        .invoke("SendMessage", this.user, this.message)
        .catch(function (err) {
          return console.error(err.toString());
        });
    },
    //获取在线用户
    getUsers() {
      this.connection.invoke("GetUsers").catch(function (err) {
        return console.error(err.toString());
      });
    },
  },
};
</script>

<style>
</style>
