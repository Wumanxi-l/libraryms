using Model;
using BLL;
using System;
using PropertyMS.App_Start;
using System.Web.Mvc;

namespace BookManagement.Controllers
{
     [AdminAuthorize]
    public class ClassifyController : Controller
    {
        private readonly ClassifyBLL _classifyBLL;

        public ClassifyController()
        {
            _classifyBLL = new ClassifyBLL();
        }

        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var books = _classifyBLL.GetClassify();
            return View(books);
        }

        /// <summary>
        /// 创建分类页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 创建分类操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Classify model)
        {
            if (ModelState.IsValid)
            {
                _classifyBLL.EditClassify(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            _classifyBLL.DeleteClassify(id);
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

            var data = _classifyBLL.GetClassifyById(id);
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            return View(data);
        }

        /// <summary>
        /// 编辑分类操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Classify model)
        {
            if (ModelState.IsValid)
            {
                var data = _classifyBLL.EditClassify(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
