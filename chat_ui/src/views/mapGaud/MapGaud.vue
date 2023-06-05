<template>
  <van-col :span="24" class="MapBox">
    <div id="mapContainer" :style="{ height: WindowHeight + 'px' }" />
  </van-col>
</template>

<script>
export default {
  name: "MapGaud",
  data() {
    return {
      WindowHeight: "",
      DataList: "",
      Signal: require("@/assets/images/markrs.png"),
    };
  },
  created() {
    this.getWindowHeight();
    window.addEventListener("resize", this.getWindowHeight);
  },
  mounted() {
    this.initMap();
  },
  methods: {
    initMap() {
      const chat = this;
      // eslint-disable-next-line no-undef
      var map = new AMap.Map("mapContainer", {
        zoom: 11, // 级别
        center: [121.499803, 31.239613], // 中心点坐标
        viewMode: "3D", // 使用3D视图
      });
      // 移除已创建的 marker
      var markers = [];
      // https://webapi.amap.com/theme/v1.3/markers/n/mark_rs.png
      // 创建一个 Marker 实例：
      setTimeout(() => {
        var content =
          "" +
          '<div class="custom-content-marker" style="position: relative">' +
          '   <img src="' +
          chat.Signal +
          '">' +
          '<div class="close-btn" style="position: absolute;top:50%;left:50%;transform: translate(-50%, -95%);font-size:12px;color:#fcf9f2">' +
          "蛤" +
          "</div>";
        ("</div>");
        // eslint-disable-next-line no-undef
        var marker = new AMap.Marker({
          position: [121.499803, 31.239613],
          content: content,
        });
        marker.content =
          '<div style="padding:0px"><h4 style="padding:0px;margin:5px">' +
          "我就随便放个标签" +
          "</h4>" +
          "<div>";
        marker.on("click", markerClick);
        markers.push(marker);
        // 将创建的点标记添加到已有的地图实例：
        map.add(markers);
      }, 10);
      // eslint-disable-next-line no-undef
      var infoWindow = new AMap.InfoWindow({ offset: new AMap.Pixel(0, -30) });
      function markerClick(e) {
        infoWindow.setContent(e.target.content);
        infoWindow.open(map, e.target.getPosition());
      }
    },
    getWindowHeight() {
      this.WindowHeight = window.innerHeight - 40;
    },
  },
};
</script>

<style scoped>
.MapBox {
  position: absolute;
}
#mapContainer {
  position: relative;
}
</style>
