import axios from 'axios'
// import store from '@/store/index.js'
import router from '../../router'
// import {
//     getToken
// } from '@/api/common/auth'
// 引入nprogress
import NProgress from 'nprogress'
import 'nprogress/nprogress.css' // 这个样式必须引入
var baseURL = process.env.VUE_APP_API_URL
var axiosInstance = axios.create({
    baseURL: baseURL,
    timeout: process.env.VUE_APP_TIMEOUT, // 20s
    headers: {
        'Content-Type': 'application/json'
    }
})
axiosInstance.interceptors.request.use(
    (config) => {
        NProgress.start()
        // showFullScreenLoading()
        // 请求头添加token
        // if (store.getters.token) {
        //     config.headers['Authorization'] = 'Bearer ' + getToken()
        // }
        // const token = sessionStorage.getItem('userToken')
        // if (token) {
        //   config.headers.Authorization = 'Bearer ' + token
        // }
        return config
    },
    (error) => {
        // tryHideFullScreenLoading()
        return Promise.reject(error)
    }
)

axiosInstance.interceptors.response.use(
    (response) => {
        NProgress.done()
        return response
    },
    (error) => {
        if (error.response) {
            switch (error.response.status) {
                case 401:
                    router.replace({
                        name: '404'
                    })
                    break
                case 403:
                    router.replace({
                        name: '404'
                    })
                    break
                case 500:
                    router.replace({
                        name: '404'
                    })
                    break
            }
        }
        return Promise.reject(error)
    }
)

export default axiosInstance