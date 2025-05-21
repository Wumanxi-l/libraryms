using System;
using Model;
using BLL;
using System.Security.Claims;
using System.Web.Mvc;
using LibraryMS.Models;
using System.EnterpriseServices;

namespace BookManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserBLL _userBll;
        private readonly AdminBLL _adminBll;

        public HomeController()
        {
            _userBll = new UserBLL();
            _adminBll = new AdminBLL();
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 用户登录操作
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(User user)
        {
            var model = _userBll.Login(user.UserName, user.Password);
            if (model == null)
            {
                ModelState.AddModelError("", "登录失败，用户名或密码错误");
                return View(user);
            }
            else
            {
                //写入登录用户信息进Session
                Session["UserId"] = model.Id;
                Session["UserName"] = model.UserName;

                return RedirectToAction("Index", "Book");
            }
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //判断用户名是否已存在
                if (_userBll.IsUserExisted(model.UserName))
                {
                    ModelState.AddModelError("", "该用户名已存在");
                    return View(model);
                }

                //保存用户信息
                User user = new User { 
                    UserName = model.UserName, 
                    Password = model.Password,
                    Sex = model.Sex,
                    Address = model.Address,
                    PhoneNum = model.PhoneNum,
                    CreateTime = DateTime.Now
                };
                _userBll.AddUser(user);
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "错误提示");
            return View(model);
        }

        /// <summary>
        /// 管理员登录页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminLogin()
        {
            return View();
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AdminLogin(User user)
        {
            var model = _adminBll.Login(user.UserName, user.Password);
            if (model == null)
            {
                ModelState.AddModelError("", "登录失败，用户名或密码错误");
                return View(user);
            }
            else
            {
                //写入登录用户信息进Session
                Session["AdminId"] = model.Id;
                Session["AdminName"] = model.UserName;

                return RedirectToAction("Index", "AdminBook");
            }
        }

        public ActionResult Error(int error)
        {
            var message = "出错了呀";
            if (error == 1)
            {
                message = "您不能删除您自己的账号!";
            }

            return View(new ErrorViewModel { RequestId = error.ToString(), ErrorMessage = message });
        }
    }
}
