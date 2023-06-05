<template>
  <div>
    <Frame-component />
    <div class="index">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import FrameComponent from "../components/FrameComponent.vue";

export default {
  components: {
    FrameComponent,
  },
};
</script>

<style scoped>
.index {
  padding-top: 40px;
  position: relative;
}
</style>