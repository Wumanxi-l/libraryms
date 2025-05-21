using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BorrowBLL
    {
        private readonly BorrowDAL dal;

        public BorrowBLL()
        {
            dal = new BorrowDAL();
        }

        /// <summary>
        /// 借书
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public string BorrowBook(int userId, int bookId)
        {
           return dal.BorrowBook(userId, bookId);
        }

        /// <summary>
        /// 还书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ReturnBook(int id)
        {
           return dal.ReturnBook(id);

        }

        /// <summary>
        /// 分页查询借书记录
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public IQueryable<Borrow> GetBorrows(int pageSize, int pageNo)
        {
            return dal.GetBorrows(pageSize, pageNo);
        }

        /// <summary>
        /// 分页查询用户的借书记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IQueryable<Borrow> GetUserBorrows(int userId)
        {
            return dal.GetUserBorrows(userId);
        }

        /// <summary>
        /// 查询前10名的用户借书统计数据
        /// </summary>
        /// <returns></returns>
        public List<StatisticModel> GetUserStatistic()
        {
            return dal.GetUserStatistic();
        }

        /// <summary>
        /// 查询前10名的最受欢迎的图书统计
        /// </summary>
        /// <returns></returns>
        public List<StatisticModel> GetBookStatistic()
        {
            return dal.GetBookStatistic();
        }
    }
}
