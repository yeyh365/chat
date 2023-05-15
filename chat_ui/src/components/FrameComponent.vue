<template>
  <div>
    <h2>
      <!-- <aside-component /> -->
      <header-component />
    </h2>
  </div>
</template>

<script>
// import AsideComponent from "./AsideComponent";
import HeaderComponent from "./HeaderComponent";
export default {
  name: "FrameComponent",
  components: {
    HeaderComponent,
  },
};
</script>

<style>
</style>