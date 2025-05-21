using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class Classify
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "分类名称必填")]
        [Display(Name = "分类名称")]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
