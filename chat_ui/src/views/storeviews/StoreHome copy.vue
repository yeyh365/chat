<template>
  <div>
    <h2>StoreHome</h2>
    <!-- src="http://m7.music.126.net/20230513202734/40d0e832924dac2745e28137f6cd779c/ymusic/70cb/f4e6/e2d8/20735cc875c075272f4d1d33c12dbcd8.mp3" -->
     <!-- <audio
      src="http://m7.music.126.net/20230513202734/40d0e832924dac2745e28137f6cd779c/ymusic/70cb/f4e6/e2d8/20735cc875c075272f4d1d33c12dbcd8.mp3"
      controls="controls"
      autoplay="autoplay"
      您的浏览器不支持audio标签=""
    >
      >
    </audio>  -->
       <!-- <aplayer
      :audio="audio"
      @update:volume="onListHide"
      :autoplay="false"
      fixed
      float
    /> -->
    <div style="width:100%;">
<aplayer :audio="audio" :lrcType="3" />
</div>
  </div>
</template>

<script>
import aplayer from '@moefe/vue-aplayer'
export default {
  name: "StoreHome",
  components:{
         aplayer   
  },
  data() {
    return {
      // videoUpload: {
      //           //progress: false,
      //          // progressPercent: 0,
      //          // successPercent: 0,
      //           theme: '#ffc0cb',
      //           autoplay: true,
      //           repeat: 'repeat-one', // 轮播模式。值可以是 'repeat-one'（单曲循环）'repeat-all'（列表循环）或者 'no-repeat'（不循环）。为了好记，还可以使用对应的 'music' 'list' 'none'
      //           // mini: true, // 迷你模式
      //           //float: true, // 浮动模式。你可以在页面上随意拖放你的播放器
               
      //      },
      //       music: { // 当前播放的音乐
      //               title: '能够成家吗',
      //               author: '咖啡少年',
      //               url: "http://yinheyibei.oss-cn-beijing.aliyuncs.com/yinheyibei-music/music/2021/11/12/5c5398742b50f759b80ea9f55a9a3e60.mp3",
      //               pic:'https://dss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=2366432289,1795558543&fm=26&gp=0.jpg' // 封面图片
      //           },

        audio: {
        name: '东西（Cover：林俊呈）',
        artist: '纳豆',
        url: "http://yinheyibei.oss-cn-beijing.aliyuncs.com/yinheyibei-music/music/2021/11/12/5c5398742b50f759b80ea9f55a9a3e60.mp3",
        cover: 'https://p1.music.126.net/5zs7IvmLv7KahY3BFzUmrg==/109951163635241613.jpg?param=300y300', // prettier-ignore
        lrc: 'https://cdn.moefe.org/music/lrc/thing.lrc'},
      musicCover: require("@/assets/images/logo.jpg"),
    }
  },
  methods: {
        onListHide() {},
  },
};
</script>

<style>
</style>