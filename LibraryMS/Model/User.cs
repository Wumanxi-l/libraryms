using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "用户名必填")]
        [Display(Name = "用户")]
        [StringLength(6, MinimumLength = 2, ErrorMessage = "用户名至少2个字符")]
        public string UserName { get; set; }
        [Display(Name = "性别")]
        public string Sex { get; set; }
        [Display(Name = "地址")]
        public string Address { get; set; }
        [Display(Name = "手机号")]
        public string PhoneNum { get; set; }
        [Required(ErrorMessage = "密码必填")]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Display(Name = "注册时间")]
        public DateTime CreateTime { get; set; }
    }
}
