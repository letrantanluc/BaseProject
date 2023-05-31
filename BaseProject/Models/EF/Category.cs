using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BaseProject.Models.EF
{
    public class Category
    {
        public Category() {
            this.Products= new HashSet<Product>();
            this.News = new HashSet<New>();
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Tên không được để trống")]
        [StringLength(150,ErrorMessage ="Không vượt quá 150 ký tự")]
        public string CategoryName { get; set; }

        [StringLength(500, ErrorMessage = "Không vượt quá 500 ký tự")]
        public string Description { get; set; }

        public string Alias { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
       
        public ICollection<Product> Products { get; set; }
        public ICollection<New> News { get; set; }
    }
}