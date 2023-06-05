import router from './router'
import store from './store'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style


router.beforeEach(async(to, from, next) => {
  // start progress bar
  NProgress.start()
  // determine whether the user has logged in
  const hasToken = sessionStorage.getItem('token')

  if (hasToken) {
    next(to.path)
    // if (to.path === '/login') {
    //   // if is logged in, redirect to the home page
    //   next({ path: '/' })
    //   NProgress.done() // hack: https://github.com/PanJiaChen/vue-element-admin/pull/2939
    // } else {
    //   // determine whether the user has obtained his permission roles through getInfo
    //   const hasRoles = store.getters.roles && store.getters.roles.length > 0
    //   if (hasRoles) {
    //     next()
    //   } else {
    //       // get user info
    //       // note: roles must be a object array! such as: ['admin'] or ,['developer','editor']
    //       const Data = await store.dispatch('user/getInfo')

    //       next({ ...to, replace: true })
    //   }
    // }
  } else {
    /* has no token*/
    next({ path: '/Login' })
    NProgress.done()
    // if (whiteList.indexOf(to.path) !== -1) {
    //   // in the free login whitelist, go directly
    //   next()
    // } else {
    //   // other pages that do not have permission to access are redirected to the login page.
    //   next(`/login?redirect=${to.path}`)
    //   NProgress.done()
    // }
  }
})

router.afterEach(() => {
  // finish progress bar
  NProgress.done()
})