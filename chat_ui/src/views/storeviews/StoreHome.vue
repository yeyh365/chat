<template>
  <div>
    <aplayer 
    :list ="music"
  :music="music[0]"

   @play = "play"
   @pause = "pause"
>
<!-- <template v-slot:display="{ currentMusic, playStat }">
  {{ currentMusic.title }}
  {{ playStat.duration }} -{{ playStat.playedTime }}
</template> -->
</aplayer>
  </div>
</template>

<script>
import Aplayer from 'vue-aplayer'
export default {
  name:'StoreHome',
  components: {
        Aplayer
    },
  data() {
    return {
      music:[{
     title: 'secret base~君がくれたもの~',
     artist: 'Silent Siren',
     src: 'http://yinheyibei.oss-cn-beijing.aliyuncs.com/yinheyibei-music/music/2021/11/12/5c5398742b50f759b80ea9f55a9a3e60.mp3',
     pic: 'https://p1.music.126.net/5zs7IvmLv7KahY3BFzUmrg==/109951163635241613.jpg?param=300y300'
  },
  {
     title: '张震岳',
     artist: '小宇',
     src: 'https://localhost:7278/Music/%E5%BC%A0%E9%9C%87%E5%B2%B3-%E5%B0%8F%E5%AE%87.mp3',
     pic: 'https://p1.music.126.net/5zs7IvmLv7KahY3BFzUmrg==/109951163635241613.jpg?param=300y300'
  }]
    };
   
  },
   methods: {
      play(){
        console.log("播放时触发")
      },
      pause(){
        console.log("暂定")
      }
    },
};
</script>

<style scoped>
/* 根据需要自定义样式 */
 button {
          display: inline-block;
        }
</style>