using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace DAL
{
    public class BookDAL
    {
        private readonly MyDbContext db;

        public BookDAL()
        {
            db = new MyDbContext();
        }

        /// <summary>
        /// 添加图书
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        public bool AddBook(Book model)
        {
            //添加
            model.CreateTime = DateTime.Now;
            db.Books.Add(model);
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 编辑图书
        /// </summary>
        /// <param name="Book"></param>
        /// <returns></returns>
        public bool UpdateBook(Book model)
        {
            var book = db.Books.FirstOrDefault(x => x.Id == model.Id);
            book.Name = model.Name;
            book.ISBN = model.ISBN;
            book.Position = model.Position;
            book.Press = model.Press;
            book.Price = model.Price;
            book.Amount = model.Amount;
            book.ClassifyId = model.ClassifyId;

            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询图书详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book GetBook(int id)
        {
            return db.Books.Include(x => x.Classify).FirstOrDefault(x => x.Id == id);
        }


        /// <summary>
        /// 查询图书列表
        /// </summary>
        /// <param name="keyword"></param>
        public IQueryable<Book> GetBooks(string keyword)
        {
            var query = db.Books.AsQueryable();

            //模糊查询关键字
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            var data = query.Include(x => x.Classify).OrderBy(x => x.Id);
            return data;
        }
        
        /// <summary>
        /// 删除图书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteBook(int id)
        {
            var book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null) return false;

            //如果图书已借出，则禁止删除
            if (db.Borrows.Any(x => x.BookId == id && x.IsReturn == false))
            {
                return false;
            }

            //执行删除
            db.Books.Remove(book);        
            return db.SaveChanges() > 0;
        }
    }
}
