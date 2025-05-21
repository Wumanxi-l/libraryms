using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Book
    {
        [Key]
        [Display(Name = "图书Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "书名必填")]
        [Display(Name = "书名")]
        public string Name { get; set; }
        [Required(ErrorMessage = "ISBN必填")]
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "出版社必填")]
        [Display(Name = "出版社")]
        public string Press { get; set; }
        [Display(Name = "分类")]
        public int ClassifyId { get; set; }
        [Display(Name = "采购价")]
        public decimal Price { get; set; }
        [Display(Name = "数量")]
        public int Amount { get; set; }
        [Display(Name = "位置")]
        public string Position { get; set; }
        [Display(Name = "入馆时间")]
        public DateTime CreateTime { get; set; }

        [ForeignKey("ClassifyId")]
        public virtual Classify Classify { get; set; }
    }
}
