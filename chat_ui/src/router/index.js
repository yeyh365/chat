import VueRouter from 'vue-router'
import Vue from 'vue'
Vue.use(VueRouter)
const router = new VueRouter({
    routes: [{
        path: '/',
        redirect: 'HelloWorld',
        component: () => import('@/components/HelloWorld.vue'),
    }, {
        path: '/HelloWorld',
        name: 'HelloWorld',
        component: () => import('@/components/HelloWorld.vue'),
    }]
})

export default router