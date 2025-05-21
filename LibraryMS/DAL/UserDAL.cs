using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class UserDAL
    {
        private readonly MyDbContext db;

        public UserDAL()
        {
            db = new MyDbContext();
        }
        
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public User Login(string userName, string pwd)
        {
            return db.Users.FirstOrDefault(x => x.UserName == userName && x.Password == pwd);
        }

        /// <summary>
        /// 判断用户名是否已存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsUserExisted(string userName)
        {
            return db.Users.Any(x => x.UserName == userName);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool AddUser(User model)
        {
            //添加
            db.Users.Add(model);
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool UpdateUser(User model)
        {
            //添加
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(int id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public IQueryable<User> GetUsers(string keyword, int pageSize, int pageNo)
        {
            var query = db.Users.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.UserName.Contains(keyword));
            }
            var data = query.OrderBy(x => x.Id);
            return data;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null) return false;

            //删除该用户的借书记录
            var data = db.Borrows.Where(x => x.UserId == userId).ToList();
            data.ForEach(x => db.Borrows.Remove(x));

            //删除使用户
            db.Users.Remove(user);
            return db.SaveChanges() > 0;
        }
    }
}
