using System.ComponentModel.DataAnnotations;

namespace LibraryMS.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "用户名必填")]
        [Display(Name = "用户")]
        [StringLength(6, MinimumLength = 3, ErrorMessage = "用户名至少2个字符")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "密码必填")]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "两次密码输入不匹配")]
        [Display(Name = "密码确认")]
        public string Password2 { get; set; }

        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "手机号")]
        public string PhoneNum { get; set; }

        [Display(Name = "性别")]
        public string Sex { get; set; }
    }
}
