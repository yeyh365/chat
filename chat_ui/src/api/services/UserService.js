import axiosInstance from '../common/export'

export default {
  // Post /APublic/GetNginxOPENAI  测试apiapi/Auth/Login
  Login(info) {
      return new Promise((resolve, reject) => {
          axiosInstance.get(`/Auth/Login`, {
            params: {
              Name: info
            }
              })
              .then(response => {
                  resolve(response.data)
              })
              .catch(err => {
                  reject(err)
              })
      })
  }
}