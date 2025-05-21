using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class UserBLL
    {
        private readonly UserDAL dal;

        public UserBLL()
        {
            dal = new UserDAL();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public User Login(string userName, string pwd)
        {
            return dal.Login(userName, pwd);
        }

        /// <summary>
        /// 判断用户名是否已存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsUserExisted(string userName)
        {
            return dal.IsUserExisted(userName);
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool AddUser(User model)
        {
            return dal.AddUser(model);
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public bool UpdateUser(User model)
        {
            return dal.UpdateUser(model);
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUser(int id)
        {
            return dal.GetUser(id);
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
            return dal.GetUsers(keyword, pageSize, pageNo);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            return dal.DeleteUser(id);
        }
    }
}
