using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class ClassifyBLL
    {
        private readonly ClassifyDAL dal;

        public ClassifyBLL()
        {
            dal = new ClassifyDAL();
        }

        /// <summary>
        /// 查询分类
        /// </summary>
        /// <returns></returns>
        public Classify GetClassifyById(int id)
        {
            return dal.GetClassifyById(id);
        }

        /// <summary>
        /// 查询所有分类
        /// </summary>
        /// <returns></returns>
        public List<Classify> GetClassify()
        {
            return dal.GetClassify();
        }

        /// <summary>
        /// 添加或编辑分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool EditClassify(Classify model)
        {
            return dal.EditClassify(model);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteClassify(int id)
        {
            return dal.DeleteClassify(id);
        }
    }
}
