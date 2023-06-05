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
                path: '/MapGaud',
                name: '发现',
                component: () => import('../views/mapGaud/MapGaud.vue')
            },
            {
                path: '/StoreHome',
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
    }, {
        path: '/Login',
        name: 'Login',
        component: () => import('../views/Login.vue'),
    }, {
        path: '/Register',
        name: 'Register',
        component: () => import('../views/Register.vue'),
    }

]

const router = new VueRouter({
    mode: 'hash',
    base: process.env.BASE_URL,
    routes
})
router.beforeEach((to, from, next) => {
    if(to.path=="/Login"){
        next()
    }else{
        const hasToken = sessionStorage.getItem('token')
        if (hasToken === null || hasToken === "") {     
            next("/Login");  
        } else {      
            next();    
        }  
  }})
  
  // 全局后置路由守卫———— 初始化的时候被调用、 每次路由切换之后被调用
  router.afterEach((to, from) => {
    document.title =  'CHAT系统'
  })
export default router