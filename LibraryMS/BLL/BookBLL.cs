
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BookBLL
    {
        private readonly BookDAL dal;

        public BookBLL()
        {
            dal = new BookDAL();
        }

        /// <summary>
        /// 添加图书
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        public bool AddBook(Book model)
        {
            return dal.AddBook(model);
        }

        /// <summary>
        /// 编辑图书
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        public bool UpdateBook(Book model)
        {
            return dal.UpdateBook(model);
        }

        /// <summary>
        /// 查询图书详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book GetBook(int id)
        {
            return dal.GetBook(id);
        }

        /// <summary>
        /// 分页查询图书列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IQueryable<Book> GetBooks(string keyword)
        {
            return dal.GetBooks(keyword);
        }

        /// <summary>
        /// 删除图书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteBook(int id)
        {
            return dal.DeleteBook(id);
        }
    }
}
