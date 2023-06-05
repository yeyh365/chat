using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Core.Enums
{
    public enum ResultCode
    {

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 200,

        /// <summary>
        /// 未注册
        /// </summary>
        [Description("未注册")]
        Unregistered = 101,

        /// <summary>
        /// encodingAesKey 非法
        /// </summary>
        [Description("encodingAesKey 非法")]
        encodingAesKey = -41001,

        /// <summary>
        /// encodingAesKey 非法
        /// </summary>
        [Description("encodingAesKey 非法")]
        encodingIv = -41002,

        /// <summary>
        /// aes 解密失败
        /// </summary>
        [Description("aes 解密失败")]
        aesNotFound = -41003,

        /// <summary>
        /// 解密后得到的buffer非法
        /// </summary>
        [Description("解密后得到的buffer非法")]
        FailValidate = -41004,



        /// <summary>
        /// base64加密失败
        /// </summary>
        [Description("base64加密失败")]
        base64De = -41005,

        /// <summary>
        /// base64解密失败
        /// </summary>
        [Description("base64解密失败")]
        base64En = -41016,


        /// <summary>
        /// NotFound
        /// </summary>
        [Description("NotFound")]
        NotFound = 801,

        /// <summary>
        /// InnerException
        /// </summary>
        [Description("InnerException")]
        InnerException = 802,

    }
}
