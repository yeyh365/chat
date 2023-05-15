import axiosInstance from '../common/export'

export default {
  // Post /APublic/GetNginxOPENAI  测试api
  Openai(info) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/APublic/GetNginxOPENAI`, {
          params: {
            input: info
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