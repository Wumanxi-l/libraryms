using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class AdminDAL
    {
        private readonly MyDbContext db;

        public AdminDAL()
        {
            db = new MyDbContext();
        }
        
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Admin Login(string userName, string pwd)
        {
            return db.Admins.FirstOrDefault(x => x.UserName == userName && x.Password == pwd);
        }
    }
}
