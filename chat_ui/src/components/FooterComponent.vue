<template>
  <div class="footer">
    <van-tabbar
      v-model="active"
      active-color="#4690E5"
      inactive-color="#BAB9BE"
    >
      <van-tabbar-item
        name="smile-comment-o"
        icon="smile-comment-o"
        to="/PublicChat"
        >公共频道</van-tabbar-item
      >
      <van-tabbar-item name="tv-o" icon="tv-o" to="/OpenAI"
        >OPANAI</van-tabbar-item
      >
      <van-tabbar-item name="bulb-o" icon="bulb-o" to="/find"
        >发现</van-tabbar-item
      >
      <van-tabbar-item name="shop-o" icon="shop-o" to="/store"
        >会员模式</van-tabbar-item
      >
      <van-tabbar-item name="friends-o" icon="friends-o" to="/me"
        >我的</van-tabbar-item
      >
    </van-tabbar>
  </div>
</template>

<script>
export default {
  name: "FooterComponent",
  data() {
    return {
      active: "home",
    };
  },
};
</script>

<style>
</style>