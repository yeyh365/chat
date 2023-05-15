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
var connection = new signalR.HubConnectionBuilder()
  .withUrl("https://localhost:7278/chatHub")
  .withAutomaticReconnect() //断线自动重连
  .build();

connection.start();

//自动重连成功后的处理
connection.onreconnected((connectionId) => {
  alert(connectionId);
});

export default {
  name: "StoreHome",
  data() {
    return {
      user: "",
      pwd: "",
      remsg: ["test", "test3"],
      message: "",
      onlineUser: ["testUser"],
    };
  },
  created() {
    var chat = this;
    connection.on("SendMessageResponse", function (res) {
      if (res && res.status == 0) {
        chat.remsg.push(res.message);
        // var li = document.createElement("li");
        // li.textContent = res.message;
        // document.getElementById("messagesList").appendChild(li);
      } else {
        alert(res.message);
      }
    });
    //---消息---

    connection.on("LoginResponse", function (res) {
      if (res && res.status == 0) {
        // sessionStorage.setItem("curuser", res.data);
        alert(res.message);
        chat.getUsers();
      } else {
        alert("登录失败！");
      }
    });
    //---登录---
  },
  methods: {
    login() {
      console.log("logon");
      connection.invoke("Login", this.user, this.pwd).catch(function (err) {
        return console.error(err.toString());
      });
    },
    handle() {
      connection
        .invoke("SendMessage", this.user, this.message)
        .catch(function (err) {
          return console.error(err.toString());
        });
    },
    //获取在线用户
    getUsers() {
      connection.invoke("GetUsers").catch(function (err) {
        return console.error(err.toString());
      });
      connection.on("GetUsersResponse", (res) => {
        if (res && res.status == 0) {
          res.onlineUser.forEach((item) => {
            this.onlineUser.push(item.userName);
          });
          console.log("onlineUser", res);
          console.log(this.onlineUser);
        }
      });
    },
  },
};
</script>

<style>
</style>
