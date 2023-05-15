/**
 * Created by caohong on 7/4/2018.
 */
// import enums from '@/api/common/enums.js'
const utils = {}

utils.subText = function (text) {
    const newText = text.substr(0, 5) + '...'
    return newText
}
// 中英文混合字符串截取
utils.subStrText = function (str, n) {
    const r = "/[^\x00-\xff]/g"
    if (str.replace(r, 'mm').length <= n) {
        return str
    }
    var m = Math.floor(n / 2)
    for (let i = m; i < str.length; i++) {
        if (str.substr(0, i).replace(r, 'mm').length >= n) {
            return str.substr(0, i) + '...'
        }
    }
    return str
}
// 转换后台传过来带T的日期(新方法，之后有用到用新方法)
utils.formatDateTime = function (date) {
    if (date !== null && date !== '' && date !== undefined) {
        const newDate = new Date(date)
        return new Date(newDate.getTime() + 8 * 3600 * 1000)
            .toISOString()
            .replace(/T/g, ' ')
            .replace(/\.[\d]{3}Z/, '')
    } else {
        return ''
    }
}
utils.formatDate = function (date) {
    if (date !== null && date !== '' && date !== undefined) {
        const newDate = new Date(date)
        return new Date(newDate.getTime() + 8 * 3600 * 1000)
            .toISOString()
            .replace(/T/g, ' ')
            .split(' ')[0]
    } else {
        return ''
    }
}
// 点击月份减一
utils.MonthReduce = function (date) {
    let currentDate = date
    currentDate = new Date(currentDate)
    let lastDate = currentDate.setMonth(currentDate.getMonth() - 3) // 输出日期格式为毫秒形式1551398400000
    lastDate = new Date(lastDate)
    return lastDate
}

// 判断两个时间  判断结束时间是否大于开始时间
utils.CompareDate = function (curDate, startDate) {
    return (
        new Date(curDate.replace(/-/g, '/')) >
        new Date(startDate.replace(/-/g, '/'))
    )
}
// 过滤二级路由
utils.getSubMenus = function (menus, name) {
    const result = []
    utils.getResource(menus, name, result)
    const menuList = result[0].children
    return menuList
}
// 菜单判断
utils.getResource = function (menu, name, result) {
    if (menu) {
        for (const item of menu) {
            if (item.children && item.children.length) {
                utils.getResource(item.children, name, result)
            }
            if (item.name === name) {
                result.push(item)
            }
        }
    }
}

utils.inArray = function (search, array) {
    for (var i in array) {
        if (array[i] === search) {
            return true
        }
    }
    return false
}
export default utils