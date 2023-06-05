<template>
  <div >
    <p class="title">登录</p>
    <van-image
     class="bigBox"
      round
      width="6rem"
      height="6rem"
      :src="purl+imgUrl"
    />
    <van-form>
      <!-- <van-field
        v-model="username"
        name="用户名"
        label="用户名"
        placeholder="用户名"
        :rules="[{ required: true, message: '请填写用户名' }]"
      />
      <van-field
        v-model="password"
        type="password"
        name="密码"
        label="密码"
        placeholder="密码"
        :rules="[{ required: true, message: '请填写密码' }]"
      /> -->
           <van-field
              readonly
              clickable
              validate-trigger="onChinge"
              name="picker"
              :value="username"
              label="随机登录昵称"
              placeholder="请选择你登录昵称"
              @click="showPicker = true"
            />
            <van-popup v-model="showPicker" position="bottom">
              <van-picker
                show-toolbar
                :columns="columns"
                @confirm="onConfirm"
                @cancel="showPicker = false"
              />
            </van-popup>
      <div style="margin: 16px">
        <van-button
          round
          block
          type="info"
          native-type="submit"
          @click="onSubmit"
          >登录</van-button
        >
      </div>
      <!-- <div class="reg">
        <div @click="toRegister">没有账号？立即注册</div>
      </div> -->
    </van-form>
  </div>
</template>

<script>
import DictDataService from "@/api/services/DictDataService";
import UserService from "@/api/services/UserService";
import { Toast } from "vant";
export default {
  name: "Login",
  data() {
    return {
      username: "",
      password: "",
      purl:(process.env.VUE_APP_API_URL).slice(0, -3),
      imgUrl:"",
      columns: [],
      showPicker: false,
      NameListData:[]
    };
  },
  created() {
    this.GetNameListData();
  },
  methods:
   {    onConfirm(value) {
   console.log('NameListData',this.NameListData)
  //  const result="";
    this.NameListData.forEach(item=>{
    if(item.value===value){
    this.username = value;
    this.imgUrl=item.remark;
    }

   })
      this.showPicker = false;
    },
    onSubmit() {
      /*console.log('submit', values);*/
      if (this.username !="") {
        UserService.Login(this.username).then((res)=>{
                  console.log("resname", res);
                  if(res.code==200){
                  sessionStorage.setItem('token', res.data.token);
                  sessionStorage.setItem('name', res.data.name);
                                    sessionStorage.setItem('photo', res.data.photo);
                          
        Toast.success("登录成功");
        Toast("请选择账号或密码");
                  }

        }).then(()=>{
           this.$router.push("PublicChat");
        })

      } else if (this.username == "" && this.password == "") {
        Toast("请选择账号或密码");
      } else {
        Toast.fail("账号或密码错误");
      }
    },
    toRegister() {
      this.$router.push("/Register");
    },
    //获取后端随机的名字
    GetNameListData() {
      DictDataService.dictData("RandomName").then((res) => {
        console.log("resname", res);
        this.NameListData=res.data
          res.data.forEach(item => {
            this.columns.push(item.value)
          });
        // //随机获取一个名字
        // const id = Math.round(Math.random() * res.data.length);
        // const RandomUser = res.data[id];
        // this.userName = RandomUser.value;
        // this.Photo = RandomUser.remark;
        // alert(this.userName);
        // if (res.Code === 200) {
        //   console.log(111);
        // } else {
        //   console.log(111);
        // }
      });
    },
  },
};
</script>

<style scoped>
.title {
  /* border-radius: 15px; */
  size: 1px;
  height: 50px;
  line-height: 50px;
  background-color: #20a0ff;
  color: #fff;
  text-align: center;
}
.bigBox{
  margin-top: 30%;
  margin-left:40%
}
</style>

