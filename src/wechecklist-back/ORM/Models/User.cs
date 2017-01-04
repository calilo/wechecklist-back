using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wechecklist_back.ORM.Models
{
    public class User
    {
        [Key]
        public long UserId { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50)]
        public string OpenId { get; set; }

        [Required]
        [MaxLength(11)]
        public int UserType { get; set; } = 10;

        public string HeadImgUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Token { get; set; }

        [Required]
        public DateTime RefreshDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public DateTime CreateTime { get; set; }

        public long? CreateUserId { get; set; }
    }
}
