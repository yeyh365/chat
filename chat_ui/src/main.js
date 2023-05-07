import Vue from 'vue'
import App from './App.vue'
// import 'normalize.css/normalize.css' // a modern alternative to CSS resets
import store from './store'
import router from './router'
Vue.config.productionTip = false

new Vue({
  el: '#app',
  store,
  router,
  render: h => h(App),
})