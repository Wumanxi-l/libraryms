using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class BorrowDAL
    {
        private readonly MyDbContext db;

        public BorrowDAL()
        {
            db = new MyDbContext();
        }

        /// <summary>
        /// 借书
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public string BorrowBook(int userId, int bookId)
        {
            //检测在馆图书数量
            var book = db.Books.FirstOrDefault(x => x.Id == bookId);
            if (book == null || book.Amount == 0)
            {
                return "该图书已被全部借出";
            }

            //检查用户已借数量，最多能借10本书
            var userBorrow = db.Borrows.Count(x => x.UserId == userId && x.IsReturn == false);
            if (userBorrow >= 10)
            {
                return "您已达到最大可借书数量";
            }

            //生成借书记录
            //应还书时间，最多可借书1个月
            var dueTime = DateTime.Now.AddMonths(1);
            var model = new Borrow()
            {
                UserId = userId,
                BookId = bookId,
                BorrowTime = DateTime.Now,
                DueTime = dueTime,
                IsReturn = false
            };
            db.Borrows.Add(model);

            //更新图书在管数量
            book.Amount--;

            if (db.SaveChanges() > 0)
            {
                return "借书成功";
            }
            else
            {
                return "借书失败，请重试";
            }
        }

        /// <summary>
        /// 还书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ReturnBook(int id)
        {
            var model = db.Borrows.FirstOrDefault(x => x.Id == id);
            if (model == null) return false;

            var book = db.Books.FirstOrDefault(x => x.Id == model.BookId);
            if (book == null) return false;

            //更新还书字段
            model.IsReturn = true;
            model.ReturnTime = DateTime.Now;
            //更新图书在管数量
            book.Amount++;

            return db.SaveChanges() > 0;

        }

        /// <summary>
        /// 分页查询借书记录
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IQueryable<Borrow> GetBorrows(int pageSize, int pageNo)
        {
            var query = db.Borrows.AsQueryable();

            //执行分页查询
            var data = query.Include(x => x.User).Include(x => x.Book).OrderByDescending(x => x.Id);
            return data;
        }

        /// <summary>
        /// 分页查询用户的借书记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<Borrow> GetUserBorrows(int userId)
        {
            var query = db.Borrows.Where(x => x.UserId == userId).AsQueryable();
            var data = query.Include(x => x.User).Include(x => x.Book).OrderByDescending(x => x.Id);
            return data;
        }

        /// <summary>
        /// 查询前10名的用户借书统计数据
        /// </summary>
        /// <returns></returns>
        public List<StatisticModel> GetUserStatistic()
        {
            var query = (from a in db.Borrows
                         group a by a.UserId into g
                         select new StatisticModel()
                         {
                             Id = g.Key,
                             Value = g.Count()
                         }).OrderByDescending(x => x.Value).Take(10).ToList();

            var userIds = query.Select(x => x.Id).ToList();
            var users = db.Users.Where(x => userIds.Contains(x.Id)).ToList();

            foreach(var item in query)
            {
                item.Name = users.FirstOrDefault(x => x.Id == item.Id).UserName;
            }

            return query;
        }

        /// <summary>
        /// 查询前10名的最受欢迎的图书统计
        /// </summary>
        /// <returns></returns>
        public List<StatisticModel> GetBookStatistic()
        {
            var query = (from a in db.Borrows
                         group a by a.BookId into g
                         select new StatisticModel()
                         {
                             Id = g.Key,
                             Value = g.Count()
                         }).OrderByDescending(x => x.Value).Take(10).ToList();

            var bookIds = query.Select(x => x.Id).ToList();
            var books = db.Books.Where(x => bookIds.Contains(x.Id)).ToList();

            foreach (var item in query)
            {
                item.Name = books.FirstOrDefault(x => x.Id == item.Id).Name;
            }

            return query;
        }
    }
}
