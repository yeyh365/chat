using Chat.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Chat.Domain.Entities
{
    /// <summary>
    /// 聊天记录
    /// </summary>
    [Table("chatMes", Schema = "dbo")]
    public class ChatMes : LogicDeletableEntity, IEntitiy<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 发送者
        /// </summary>
        [Column("sender")]
        public string? Sender { get; set; } = string.Empty;
        /// <summary>
        /// 发送者
        /// </summary>
        [Column("photo")]
        public string? Photo { get; set; } = string.Empty;
        /// <summary>
        /// 接收者
        /// </summary>
        [Column("receiver")]
        public string? Receiver { get; set; } = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        public string? Content { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        [Column("type")]
        public string? Type { get; set; } = string.Empty;
        /// <summary>
        /// 发送时间
        /// </summary>
        [Column("timestamp")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// 消息Id
        /// </summary>
        [Column("messageID")]
        public string? MessageID { get; set; } = string.Empty;
        /// <summary>
        /// 聊天状态
        /// </summary>
        [Column("messageStatus")]
        public string? MessageStatus { get; set; } = string.Empty;
        /// <summary>
        /// 聊天会话ID
        /// </summary>
        [Column("chatSessionID")]
        public string? ChatSessionID { get; set; } = string.Empty;
        /// <summary>
        /// 媒体文件
        /// </summary>
        [Column("mediaFiles")]
        public string? MediaFiles { get; set; } = string.Empty;
        /// <summary>
        /// 表情
        /// </summary>
        [Column("emojiSymbols")]
        public string? EmojiSymbols { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string? Remark { get; set; } = string.Empty;
    }
 }
