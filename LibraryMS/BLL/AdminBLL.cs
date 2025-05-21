using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class AdminBLL
    {
        private readonly AdminDAL dal;

        public AdminBLL()
        {
            dal = new AdminDAL();
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="AdminName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Admin Login(string AdminName, string pwd)
        {
            return dal.Login(AdminName, pwd);
        }
    }
}
