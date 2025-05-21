using System.ComponentModel.DataAnnotations;

namespace LibraryMS.Models
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "旧密码必填")]
        [Display(Name = "旧密码")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "密码必填")]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "两次密码输入不匹配")]
        [Display(Name = "密码确认")]
        public string Password2 { get; set; }
    }
}
