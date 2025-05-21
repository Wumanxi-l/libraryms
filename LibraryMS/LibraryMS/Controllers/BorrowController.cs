using Model;
using BLL;
using X.PagedList;
using System.Linq;
using Newtonsoft.Json;
using System.Web.Mvc;
using PropertyMS.App_Start;

namespace BookManagement.Controllers
{
    public class BorrowController : Controller
    {
        private readonly BookBLL _bookBll;
        private readonly BorrowBLL _borrowBll;

        public BorrowController()
        {
            _bookBll = new BookBLL();
            _borrowBll = new BorrowBLL();
        }

        /// <summary>
        /// 用户借书记录页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [UserAuthorize]
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            //获取登录用户ID
            var userIdStr = (Session["UserId"] ?? "").ToString();
            int.TryParse(userIdStr, out int userId);

            var books = _borrowBll.GetUserBorrows(userId);
            return View(books.ToPagedList(page, pageSize));
        }

        /// <summary>
        /// 后台借书记录页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [AdminAuthorize]
        public ActionResult Records(int page = 1, int pageSize = 10)
        {
            var books = _borrowBll.GetBorrows(pageSize, page);
            return View(books.ToPagedList(page, pageSize));
        }

        /// <summary>
        /// 还书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [UserAuthorize]
        public ActionResult Return(int id)
        {
            _borrowBll.ReturnBook(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 管理员还书操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         [AdminAuthorize]
        public ActionResult AdminReturn(int id)
        {
            _borrowBll.ReturnBook(id);
            return RedirectToAction("Records");
        }

        /// <summary>
        /// 用户借书统计
        /// </summary>
        /// <returns></returns>
         [AdminAuthorize]
        public ActionResult UserStatistic()
        {
            return View();
        }

        public ActionResult GetUserStatistic()
        {
            var data = _borrowBll.GetUserStatistic();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 图书被借统计
        /// </summary>
        /// <returns></returns>
         [AdminAuthorize]
        public ActionResult BookStatistic()
        {
            return View();
        }

        public ActionResult GetBookStatistic()
        {
            var data = _borrowBll.GetBookStatistic();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
