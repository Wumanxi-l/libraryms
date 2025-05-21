using Model;
using BLL;
using X.PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using PropertyMS.App_Start;
using LibraryMS.Models;

namespace BookManagement.Controllers
{ 
    public class UserController : Controller
    {
        private readonly UserBLL _userBLL;

        public UserController()
        {
            _userBLL = new UserBLL();
        }
        
        /// <summary>
        /// 用户管理页面
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult Index(string searchKey, int page = 1, int pageSize = 10)
        {
            try
            {
                var books = _userBLL.GetUsers(searchKey, pageSize, page);
                return View(books.ToPagedList(page, pageSize));
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// 添加用户页面
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 添加用户操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AdminAuthorize]
        [HttpPost]
        public ActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //判断用户名是否已存在
                if (_userBLL.IsUserExisted(model.UserName))
                {
                    ModelState.AddModelError("UserName", "该用户名已存在");
                    return View(model);
                }

                //保存用户信息
                User user = new User
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Sex = model.Sex,
                    Address = model.Address,
                    PhoneNum = model.PhoneNum,
                    CreateTime = DateTime.Now
                };
                _userBLL.AddUser(user);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [AdminAuthorize]
        public ActionResult Delete(int id)
        {
            _userBLL.DeleteUser(id);
            return RedirectToAction("Index");
        }

         [AdminAuthorize]
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var data = _userBLL.GetUser(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AdminAuthorize]
        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var user = _userBLL.GetUser(model.Id);
                //判断用户名是否已存在
                if (_userBLL.IsUserExisted(model.UserName) && model.UserName != user.UserName)
                {
                    ModelState.AddModelError("UserName", "该用户名已存在");
                    return View(model);
                }

                user.Address = model.Address;
                user.UserName = model.UserName;
                user.PhoneNum = model.PhoneNum;
                user.Sex = model.Sex;

                var data = _userBLL.UpdateUser(user);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// 修改密码页
        /// </summary>
        /// <returns></returns>
        [UserAuthorize]
        [HttpGet]
        public ActionResult UpdatePwd()
        {
            return View();
        }

        /// <summary>
        /// 修改密码操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [UserAuthorize]
        [HttpPost]
        public ActionResult UpdatePwd(PasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //获取登录用户ID
                var userIdStr = (Session["UserId"] ?? "").ToString();
                int.TryParse(userIdStr, out int userId);

                var user = _userBLL.GetUser(userId);
                if (user.Password != model.OldPassword)
                {
                    ModelState.AddModelError("OldPassword", "旧密码输入不正确");
                    return View(model);
                }

                user.Password = model.Password;
                _userBLL.UpdateUser(user);
                ViewBag.Msg = "修改成功";
            }

            return View(model);
        }
    }
}
