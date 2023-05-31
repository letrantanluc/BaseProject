using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseProject.Models.EF
{
    public class New
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "TIêu đề không được để trống")]
        [StringLength(150, ErrorMessage = "Không vượt quá 150 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Description { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Chi tiết không được để trống")]
        public string Detail { get; set; }

        public string Images { get; set; }

        public bool IsActive { get; set; }

        public string Alias { get; set; }

        public int CategoryId { get; set; } = 0;

        public Category Categories { get; set; }
    }
}