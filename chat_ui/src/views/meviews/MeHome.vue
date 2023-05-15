<template>
  <div>
    <!-- <div class="user">
      <van-row class="user-info">
        <van-col span="6" class="user-acvtor">
          <img :src="avatarSrc" alt="" />
        </van-col>
        <van-col span="16" class="user-name" @click="showLoginDialog">
          {{ nickname }}
        </van-col>
        <van-col span="2">
          <van-icon name="arrow" @click="loginOut" />
        </van-col>
      </van-row>
    </div> -->
    <div class="grid">
      <van-grid square :column-num="3">
        <van-grid-item icon="balance-list-o" text="我的订单" @click="noThing" />
        <van-grid-item icon="coupon-o" text="优惠卷" @click="noThing" />
        <van-grid-item icon="point-gift-o" text="礼品卡" @click="noThing" />
        <van-grid-item icon="star-o" text="我的收藏" @click="noThing" />
        <van-grid-item icon="flag-o" text="我的足迹" @click="noThing" />
        <van-grid-item icon="vip-card-o" text="会员福利" @click="noThing" />
        <van-grid-item icon="location-o" text="地址管理" @click="noThing" />
        <van-grid-item icon="shield-o" text="账号安全" @click="noThing" />
        <van-grid-item icon="service-o" text="联系客服" @click="noThing" />
        <van-grid-item icon="question-o" text="帮助中心" @click="noThing" />
        <van-grid-item
          icon="comment-circle-o"
          text="意见反馈"
          @click="noThing"
        />
      </van-grid>
    </div>
    <!-- <transition name="fade">
      <loginOn
        v-if="loginShow"
        @showLogin="loginShow = !loginShow"
        @onSubmit="onSubmit"
      ></loginOn>
    </transition> -->
  </div>
</template>

<script>
// import loginOn from "@/components/loginOn.vue";
// import { GoLogin } from "@/request/api";
import testServiec from "@/api/services/TestService";
export default {
  name: "MeHome",
  data() {
    return {
      //判断是否展示登陆框
      loginShow: false,
      avatarSrc:
        "https://gd-hbimg.huaban.com/3d08684c78c6bef02339f8be1ba7e1d64f6dcfd828ba-nTzqqR_fw658",
      nickname: "用户登陆",
    };
  },
  // components: {
  //   loginOn,
  // },
  mounted() {
    // this.getOpenAi();
  },
  methods: {
    getOpenAi() {
      alert(111);
      var aaa = "k2的流程";
      testServiec.Openai(aaa).then((res) => {
        if (res) {
          console.log(res);
        } else {
          console.log(res);
        }
      });
    },
    noThing() {
      this.$toast("功能未开放");
    },
    // loginUp() {
    // 	this.loginShow = true
    // },
    // showLogin() {
    // 	this.loginShow = false
    // },
    //发送登陆请求values
    async onSubmit() {
      //console.log(values["用户名"])
      // let username = values["用户名"];
      // let pwd = values["密码"];
      // const { data: res } = await GoLogin({
      //   username,
      //   pwd,
      // });
      // //把token和用户信息存到内存里
      // localStorage.setItem("token", res.token);
      // let userInfo = JSON.stringify(res.userInfo);
      // localStorage.setItem("userInfo", userInfo);
      // //把用户信息存储起来
      // this.userInfo = res.userInfo;
      // //关闭遮罩层
      // this.$toast.success("登陆成功");
      // setTimeout(() => {
      //   this.loginShow = !this.loginShow;
      //   this.nickname = res.userInfo.nickname;
      //   this.avatarSrc =
      //     "https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fc-ssl.duitang.com%2Fuploads%2Fblog%2F202107%2F19%2F20210719150601_4401e.thumb.1000_0.jpg&refer=http%3A%2F%2Fc-ssl.duitang.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1685027324&t=d10e644ea879620ccea8646e2148ed44";
      // }, 500);
      // console.log(res);
    },
    //退出登陆
    loginOut() {
      // let token = localStorage.getItem("token");
      // if (!token) return;
      // this.$dialog
      //   .confirm({
      //     title: "提示",
      //     message: "确认退出登陆",
      //   })
      //   .then((confirm) => {
      //     if (confirm == "confirm") {
      //       localStorage.removeItem("token");
      //       localStorage.removeItem("userInfo");
      //     }
      //     this.nickname = "用户登陆";
      //     this.avatarSrc =
      //       "https://gd-hbimg.huaban.com/3d08684c78c6bef02339f8be1ba7e1d64f6dcfd828ba-nTzqqR_fw658";
      //     this.$toast.success("退出成功");
      //   })
      //   .catch(() => {
      //     this.$toast("取消退出登陆");
      //   });
    },
    //展示弹出表单输入层
    showLoginDialog() {
      if (localStorage.getItem("token")) return;
      this.loginShow = !this.loginShow;
    },
  },
  created() {
    let userInfo = JSON.parse(localStorage.getItem("userInfo"));
    if (userInfo) {
      this.userInfo = userInfo;
      this.nickname = userInfo.nickname;
      this.avatarSrc =
        "https://gimg2.baidu.com/image_search/src=http%3A%2F%2Fc-ssl.duitang.com%2Fuploads%2Fblog%2F202107%2F19%2F20210719150601_4401e.thumb.1000_0.jpg&refer=http%3A%2F%2Fc-ssl.duitang.com&app=2002&size=f9999,10000&q=a80&n=0&g=0n&fmt=auto?sec=1685027324&t=d10e644ea879620ccea8646e2148ed44";
    }
  },
};
</script>

<style lang="less" scoped>
.user-info {
  height: 1.4rem;
  background: #666;
  display: flex;
  align-items: center;
  font-size: 0.22rem;
  padding: 0 0.08rem;
  color: #ccc;
  .user-acvtor {
    border-radius: 50%;
    overflow: hidden;
  }
  img {
    width: 100%;
    height: 100%;
  }
  .user-name {
    padding: 0 0.1rem;
  }
}
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active, 2.1.8 版本以下 */ {
  opacity: 0;
}
</style>
