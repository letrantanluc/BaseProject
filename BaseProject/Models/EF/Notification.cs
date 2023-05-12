using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseProject.Models.EF
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(400, ErrorMessage = "Không vượt quá 400 ký tự")]
        public string Content { get; set; }
        public string Status { get; set; }

        public DateTime Created_At { get; set; } = DateTime.Now;
    }
}