using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaseProject.Models.EF
{
    public class Product
    {

        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(500, ErrorMessage = "Không vượt quá 500 ký tự")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [StringLength(500, ErrorMessage = "Không vượt quá 500 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá  không được để trống")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Ảnh  không được để trống")]
        public string image { get; set; }

        [Required(ErrorMessage = "Tình trạng sản phẩm  không được để trống")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Số lượng  không được để trống")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Địa điểm không được để trống")]
        public string Location { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;
        public string Alias { get; set; }

        public int ViewCount { get; set; }


        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}