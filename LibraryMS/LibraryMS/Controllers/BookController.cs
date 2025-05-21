using Model;
using BLL;
using System.Web.Mvc;
using X.PagedList;
using PropertyMS.App_Start;
using System.Linq;

namespace BookManagement.Controllers
{
    [UserAuthorize]
    public class BookController : Controller
    {
        private readonly BookBLL _bookBll;
        private readonly BorrowBLL _borrowBll;

        public BookController()
        {
            _bookBll = new BookBLL();
            _borrowBll = new BorrowBLL();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ActionResult Index(string searchKey, int page = 1, int pageSize = 10)
        {
            var books = _bookBll.GetBooks(searchKey);
            return View(books.ToPagedList(page, pageSize));
        }

        /// <summary>
        /// 借书页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Borrow(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var data = _bookBll.GetBook(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }
      
        /// <summary>
        /// 借书操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Borrow(Book model)
        {
            ModelState.Remove("Name");
            ModelState.Remove("ISBN");
            ModelState.Remove("Press");

            var data = _bookBll.GetBook(model.Id);

            //获取登录用户ID
            var userIdStr = (Session["UserId"] ?? "").ToString();
            int.TryParse(userIdStr, out int userId);
            var result = _borrowBll.BorrowBook(userId, model.Id);


            ModelState.AddModelError("", result);

            return View(data);
        }
    }
}
