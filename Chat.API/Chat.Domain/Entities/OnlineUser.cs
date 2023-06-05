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
    /// 在线的用户
    /// </summary>
    [Table("onlineuser", Schema = "dbo")]
    public class OnlineUser : LogicDeletableEntity, IEntitiy<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 不是主键
        /// </summary>
        [Column("clientid")]
        public string? ClientID { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("username")]
        public string? UserName { get; set; } = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("account")]
        public string? Account { get; set; } = string.Empty;
        /// <summary>
        /// 头像
        /// </summary>
        [Column("photo")]
        public string? Photo { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        [Column("password")]
        public string? Password { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        [Column("GroupName")]
        public string? GroupName { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; } = string.Empty;
    }
}
