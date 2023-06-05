const state = {
    name: '',
    avatar: '',
    admin: '',
    introduction: '',
    roles: [],
    DicList: []
}

const mutations = {
    SET_TOKEN: (state, token) => {
        state.token = token
    },
}
const actions = {
    // user login
}

export default {
    namespaced: true,
    state,
    mutations,
    actions
}