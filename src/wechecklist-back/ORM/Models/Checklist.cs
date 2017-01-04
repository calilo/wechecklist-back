using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.ORM.Models
{
    public class Checklist
    {
        [Key]
        public long ChecklistId { get; set; }

        [Required]
        [MaxLength(45)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string MarkDown { get; set; }

        /// <summary>
        /// 10 -- new; 20: done;
        /// </summary>
        [Required]
        [MaxLength(11)]
        public int Status { get; set; } = 10;

        [Required]
        public long SubscribeUserId { get; set; }

        [Required]
        public long PublishUserId { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public long? CreateUserId { get; set; }
    }
}
