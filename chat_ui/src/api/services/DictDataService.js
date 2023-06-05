/**
 * Created by caohong on 11/09/2018.
 */
import axiosInstance from '../common/export'

export default {
  // GET /api/dictData  根据Key来获取字典数据
  dictData(Type) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Dictionary/GetDictionaryByType`, {
          params: {
            Type: Type
          }
        })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  dictDataByKey(Type, Key) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Dictionary/GetDictionaryList`, {
          params: {
            Type: Type,
            Key: Key
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