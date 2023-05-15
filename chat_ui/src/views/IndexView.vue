<template>
  <div>
    <frame-component />
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
  margin-top: 40px;
}
</style>