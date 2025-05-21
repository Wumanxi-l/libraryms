using Model;
using BLL;
using System.Web.Mvc;
using X.PagedList;
using PropertyMS.App_Start;

namespace BookManagement.Controllers
{
    [AdminAuthorize]
    public class AdminBookController : Controller
    {
        private readonly BookBLL _bookBll;
        private readonly ClassifyBLL _classifyBLL;

        public AdminBookController()
        {
            _bookBll = new BookBLL();
            _classifyBLL = new ClassifyBLL();
        }

        /// <summary>
        /// 图书管理页面
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
        /// 添加图书
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            var types = _classifyBLL.GetClassify();
            SelectList selList = new SelectList(types, "Id", "Name", 1);
            ViewBag.ClassifyList = new SelectList(types, "Id", "Name", "ClassifyId");

            return View();
        }

        /// <summary>
        /// 添加图书操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Book model)
        {
            if (ModelState.IsValid)
            {
                _bookBll.AddBook(model);
                return RedirectToAction("Index");
            }

            var types = _classifyBLL.GetClassify();
            SelectList selList = new SelectList(types, "Id", "Name", 1);
            ViewBag.ClassifyList = new SelectList(types, "Id", "Name", "ClassifyId");
            return View();
        }

        /// <summary>
        /// 删除图书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            _bookBll.DeleteBook(id);
            return RedirectToAction("Index");
        }

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

            var data = _bookBll.GetBook(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            var types = _classifyBLL.GetClassify();
            SelectList selList = new SelectList(types, "Id", "Name", 1);
            ViewBag.ClassifyList = new SelectList(types, "Id", "Name", "ClassifyId");
            return View(data);
        }

        /// <summary>
        /// 编辑图书操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Book model)
        {
            if (ModelState.IsValid)
            {
                var data = _bookBll.UpdateBook(model);
                return RedirectToAction("Index");
            }

            var types = _classifyBLL.GetClassify();
            ViewBag.ClassifyList = new SelectList(types, "Id", "Name", "ClassifyId");
            return View(model);
        }
    }
}
