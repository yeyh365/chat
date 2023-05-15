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
    /// 字典表
    /// </summary>
    [Table("dictionary", Schema = "dbo")]
    public class Dictionary : LogicDeletableEntity, IEntitiy<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        [Column("name")]
        public string? Name { get; set; } = string.Empty;

        /// <summary>
        /// key
        /// </summary>
        [Column("key")]
        public string? Key { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [Column("value")]
        public string? Value { get; set; } = string.Empty;

        /// <summary>
        /// Slot
        /// </summary>
        [Column("slot")]
        public int? Slot { get; set; }

        /// <summary>
        /// ParentKey
        /// </summary>
        [Column("parentkey")]
        public string? ParentKey { get; set; } = string.Empty;

        /// <summary>
        /// 内容备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; } = string.Empty;

    }
}
