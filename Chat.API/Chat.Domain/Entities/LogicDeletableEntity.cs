using Chat.Domain.DBFilter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Domain.Entities
{
    public abstract class LogicDeletableEntity : IDeleteFilter, IEntityCreate, IEntityUpdate
    {
        [Column("deleted", TypeName = "bit")]
        [Required]
        public bool Deleted { get; set; }

        [Column("createdby")]
        public string? CreatedBy
        {
            get; set;
        }
        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("updatedby")]
        public string? UpdatedBy
        {
            get; set;
        }

        [Column("updated")]
        public DateTime? Updated { get; set; }
    }
}
