import Vue from 'vue'
import App from './App.vue'
// import 'normalize.css/normalize.css' // a modern alternative to CSS resets
import store from './store'
import router from './router'
Vue.config.productionTip = false
// import {
//   Icon,
//   Tabbar,
//   TabbarItem,
//   Col,
//   Row,
//   Search,
//   Toast
// } from 'vant'
// Vue.use(Icon).use(Tabbar).use(TabbarItem).use(Col).use(Row).use(Search).use(Toast)
import vant from 'vant'
Vue.use(vant)
import 'vant/lib/index.css';
new Vue({
  el: '#app',
  store,
  router,
  // vant,
  render: h => h(App),
})