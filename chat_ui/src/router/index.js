import VueRouter from 'vue-router'
import Vue from 'vue'
Vue.use(VueRouter)
// const router = new VueRouter({
//     routes: [{
//         path: '/',
//         redirect: 'HelloWorld',
//         component: () => import('@/components/HelloWorld.vue'),
//     }, {
//         path: '/HelloWorld',
//         name: 'HelloWorld',
//         component: () => import('@/components/HelloWorld.vue'),
//     }]
// })
const routes = [{
        path: '/',
        name: 'PublicChat',
        redirect: '/PublicChat',
        component: () => import('../views/IndexView.vue'),
        children: [{
                path: '/PublicChat',
                name: 'PublicChat',
                component: () => import('../views/publicChat/PublicChat.vue')
            },
            {
                path: '/OpenAI',
                name: 'OpenAI',
                component: () => import('../views/openAI/OpenAI.vue')
            },
            {
                path: '/find',
                name: '发现',
                component: () => import('../views/findviews/FindHome.vue')
            },
            {
                path: '/store',
                name: '商城',
                component: () => import('../views/storeviews/StoreHome.vue')
            },
            {
                path: '/me',
                name: '我的',
                component: () => import('../views/meviews/MeHome.vue')
            },
            {
                path: "/aboutWeb",
                name: 'aboutWeb',
                component: () => import("@/views/AboutWeb.vue")
            },
        ]
    },

]

const router = new VueRouter({
    mode: 'hash',
    base: process.env.BASE_URL,
    routes
})
export default router