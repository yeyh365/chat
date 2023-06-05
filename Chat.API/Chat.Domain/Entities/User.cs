using Chat.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    /// <summary>
    /// 人员
    /// </summary>
    [Table("user", Schema = "dbo")]
    public class User : LogicDeletableEntity, IEntitiy<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        [Column("openid")]
        public string? OpenId { get; set; } = string.Empty;

        /// <summary>
        /// unionid
        /// </summary>
        [Column("unionid")]
        public string? UnionId { get; set; } = string.Empty;

        /// <summary>
        /// 最后一次设备
        /// </summary>
        [Column("deviceno")]
        public string? DeviceNo { get; set; } = string.Empty;

        /// <summary>
        /// 用户账号
        /// </summary>
        [Column("account")]
        public string? Account { get; set; } = string.Empty;

        /// <summary>
        /// 中文姓名
        /// </summary>
        [Column("name")]
        public string? Name { get; set; } = string.Empty;
        /// <summary>
        /// 手机
        /// </summary>
        [Column("password")]
        public string? Password { get; set; } = string.Empty;
        /// <summary>
        /// 手机
        /// </summary>
        [Column("phone")]
        public string? Phone { get; set; } = string.Empty;
        /// <summary>
        /// 发送者
        /// </summary>
        [Column("photo")]
        public string? Photo { get; set; } = string.Empty;

        /// <summary>
        /// 地址
        /// </summary>
        [Column("address")]
        public string? Address { get; set; } = string.Empty;
    }
}
