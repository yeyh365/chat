<template>
  <div class="wrap">
    <div
      class="content_box"
      id="box"
      ref="scrollBox"
      :style="{ height: contentHeight + 'px' }"
    >
      <div class="timer">{{ dateTime }}</div>
      <div
        :class="item.sender != userName ? 'userbox2' : 'userbox'"
        v-for="(item, index) in chatList"
        :key="index"
      >
        <div :class="item.sender != userName ? 'nameInfo2' : 'nameInfo'">
          <div style="font-size: 14px">{{ item.sender }}</div>
          <div
            :class="item.sender != userName ? 'contentText2' : 'contentText'"
          >
            {{ item.content }}
          </div>
        </div>
        <div>
          <van-image round width="50px" height="50px" :src="url + item.photo" />
        </div>
      </div>
    </div>
    <div class="bottom">
      <van-field
        v-model="inputValue"
        center
        type="textarea"
        :autosize="{ maxHeight: 100, minHeight: 25 }"
        placeholder="请输入内容"
        rows="1"
      >
        <template #button>
          <van-button size="small" type="primary" @click="handle"
            >发送</van-button
          >
        </template>
      </van-field>
    </div>
  </div>
</template>

<script>
import * as signalR from "@microsoft/signalr";
import { Toast } from "vant";
export default {
  name: "PublicChat",
  data() {
    return {
      Search: {
        Limit: 0,
        Page: 20,
      },
      userName: "",
      Pwd: "111",
      Photo: "",
      remsg: ["test", "test3"],
      message: "test1登录了",
      onlineUser: ["testUser"],
      clientId: "",
      connection: null,
      url: process.env.VUE_APP_BASE_URL,
      wssUrl: process.env.VUE_APP_BASE_URL + "chatHub",
      //聊天数据
      chatList: [],
      OldchatList: [
        {
          url: "https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg",
          username: "张三",
          content: "模拟数据123模拟数据123模拟数据123模拟数据123",
          position: "left",
        },
      ],
      //登录的用户名
      //输入内容
      inputValue: "",
      //滚动条距离顶部距离
      scrollTop: 0,
      contentHeight: "0px",
      dateTime: new Date().toISOString(),
    };
  },

  created() {
    this.getName()
    this.contentHeight = window.innerHeight - 95;
    // this.login();
    // this.GetNameListData();
  },
  mounted() {
    setTimeout(() => {
      this.login();
    }, 200);

    //创建监听内容部分滚动条滚动
    this.$refs.scrollBox.addEventListener("scroll", this.srTop);

    // this.$nextTick(() => {
    //   this.setPageScrollTo();
    // });
  },
  updated() {
    if (this.inputValue == "") {
      this.$nextTick(() => {
        this.setPageScrollTo();
      });
    }
  },
  methods: {
    login() {
      var chat = this;
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(this.wssUrl, {
          accessTokenFactory: () => {
            return "aaaaaaaa"; // 获取 JWT发送token
          },
        })
        // .withAutomaticReconnect() //断线自动重连
        // .configureReconnectBehavior((reconnectOptions) => {
        //   reconnectOptions.userProvidedAccessToken = "111111"; // 设置自定义的访问令牌
        //   reconnectOptions.retryDelays = [1000, 3000, 5000]; // 设置重新连接的延迟时间
        // })
        .build();
      this.connection
        .start()
        .then(() => {
          this.connection
            .invoke("Login", this.userName, this.Pwd, this.Photo)
            .catch(function (err) {
              return console.error(err.toString());
            });
          this.connection.on("SendMessageResponse", function (res) {
            if (res && res.status == 0) {
              console.log("SendMessageResponse", res);
              chat.remsg.push(res.message);
              chat.chatList = res.data.data.reverse();
              console.log("1111", chat.chatList);
              chat.inputValue = "";
              // var li = document.createElement("li");
              // li.textContent = res.message;
              // document.getElementById("messagesList").appendChild(li);
            } else {
                      Toast(res.message);
            }
          });
          //---消息---
          this.connection.on("LoginResponse", function (res) {
            if (res && res.status == 0) {
              // sessionStorage.setItem("curuser", res.data);
              Toast(res.message);
              chat.connection
                .invoke("SendMessage", chat.userName, chat.inputValue)
                .catch(function (err) {
                  return console.error(err.toString());
                });
            } else {
              alert("登录失败！");
            }
          });
          this.connection.on("GetUsersResponse", (res) => {
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
        })
        .catch((error) => {
          console.log("连接失败", error);
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
          alert("连接失败");
        }
      });
    },
    handle() {
      if (this.inputValue == null || this.inputValue == "") {
        return this.$toast("输入为空");
      }
      this.connection
        .invoke("SendMessage", this.userName, this.inputValue)
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
    // //返回
    // onClickLeft() {
    //   console.log("返回");
    // },
    // //更多
    // onClickRight() {
    //   console.log("按钮");
    // },
    //滚动条默认滚动到最底部
    setPageScrollTo() {
      //获取中间内容盒子的可见区域高度
      this.scrollTop = document.querySelector("#box").scrollHeight;
      setTimeout(() => {
        //加个定时器，防止上面高度没获取到，再获取一遍。
        if (this.scrollTop != this.$refs.scrollBox.scrollHeight) {
          this.scrollTop = document.querySelector("#box").scrollHeight;
        }
      }, 100);
      //scrollTop：滚动条距离顶部的距离。
      //把上面获取到的高度座位距离，把滚动条顶到最底部
      this.$refs.scrollBox.scrollTop = this.scrollTop;
    },
    //滚动条到达顶部
    srTop() {
      //判断：当滚动条距离顶部为0时代表滚动到顶部了
      if (this.$refs.scrollBox.scrollTop == 0) {
        //逻辑简介：
        //到顶部后请求后端的方法，获取第二页的聊天记录，然后插入到现在的聊天数据前面。
        //如何插入前面：可以先把获取的数据保存在 A 变量内，然后 this.chatList=A.concat(this.chatList)把数组合并进来就可以了

        //拿聊天记录逻辑:
        //第一次调用一个请求拉历史聊天记录，发请求时参数带上页数 1 传过去，拿到的就是第一页的聊天记录，比如一次拿20条。你显示出来
        //然后向上滚动到顶部时，触发新的请求，在请求中把分页数先 +1 然后再请求，这就拿到了第二页数据，然后通过concat合并数组插入进前面，依次类推，功能完成！
        console.log("到顶了，滚动条位置 :", this.$refs.scrollBox.scrollTop);
      }
    },
    sendOut() {
      console.log("发送成功");
    },
    getName(){
      const getName=sessionStorage.getItem('name');
        const getPhoto=sessionStorage.getItem('photo');
              const getToken=sessionStorage.getItem('token');    
      if(getName!=""&&getPhoto!=""&&getToken!=""){
        this.userName= getName
                this.Photo= getPhoto
      }
    }

  },
};
</script>

<style scoped>
.wrap {
  width: 100%;
  position: relative;
}
.bottom {
  min-height: 50px;
  width: 100%;
  border-top: 1px solid #eaeaea;
  position: fixed;
  bottom: 0;
}
.content_box {
  /* 
  中间栏计算高度，110是包含了上下固定的两个元素高度90
  这里padding：10px造成的上下够加了10，把盒子撑大了，所以一共是20要减掉
  然后不知道是边框还是组件的原因，导致多出了一些，这里再减去5px刚好。不然会出现滚动条到顶或者底部的时候再滚动的话就会报一个错，或者出现滚动条变长一下的bug
  */
  height: calc(100% - 135px);
  overflow: auto;
  padding: 10px;
}
.timer {
  text-align: center;
  color: #c2c2c2;
}

/* 发送的信息样式 */
/* 
右边消息思路解释：首先大盒子userbox内放两个盒子，一个放头像，一个放用户名和发送的内容，我们先用flex让他横向排列。
然后把写文字的大盒子设置flex：1。这个属性的意思就是让这个元素撑满父盒子剩余位置。然后我们再把文字盒子设置flex，并把他对齐方式设置为尾部对齐就完成了基本的结构，然后微调一下就可以了
*/
.userbox {
  width: 100%;
  display: flex;
  margin-bottom: 10px;
}
.nameInfo {
  /* 用flex：1把盒子撑开 */
  flex: 1;
  margin-right: 10px;
  /* 用align-items把元素靠右对齐 */
  display: flex;
  flex-direction: column;
  align-items: flex-end;
}
.contentText {
  background-color: #9eea6a;
  /* 把内容部分改为行内块元素，因为盒子flex：1把盒子撑大了，所以用行内块元素让内容宽度不根据父盒子来 */
  display: inline-block;
  /* 这四句是圆角 */
  border-top-left-radius: 10px;
  border-top-right-radius: 0px;
  border-bottom-right-radius: 10px;
  border-bottom-left-radius: 10px;
  /* 最大宽度限定内容输入到百分61换行 */
  max-width: 61%;
  padding: 5px 10px;
  /* 忽略多余的空白，只保留一个空白 */
  white-space: normal;
  /* 换行显示全部字符 */
  word-break: break-all;
  margin-top: 3px;
  font-size: 14px;
}

/* 接收的信息样式 */
/* 
左边消息思路解释：跟上面一样，就是换一下位置，首先通过把最外层大盒子的排列方式通过flex-direction: row-reverse;属性翻转，也就是头像和文字盒子换位置
然后删除掉尾部对齐方式，因为不写这个默认是左对齐的。我们写的左边就没必要再写了。
*/
.userbox2 {
  width: 100%;
  display: flex;
  flex-direction: row-reverse;
  margin-bottom: 10px;
}
.nameInfo2 {
  /* 用flex：1把盒子撑开 */
  flex: 1;
  margin-left: 10px;
}
.contentText2 {
  background-color: #9eea6a;
  /* 把内容部分改为行内块元素，因为盒子flex：1把盒子撑大了，所以用行内块元素让内容宽度不根据父盒子来 */
  display: inline-block;
  /* 这四句是圆角 */
  border-top-left-radius: 0px;
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
  border-bottom-left-radius: 10px;
  /* 最大宽度限定内容输入到百分61换行 */
  max-width: 61%;
  padding: 5px 10px;
  /* 忽略多余的空白，只保留一个空白 */
  white-space: normal;
  /* 换行显示全部字符 */
  word-break: break-all;
  margin-top: 3px;
  font-size: 14px;
}
</style>