using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ClassifyDAL
    {
        private readonly MyDbContext _dbContext;

        public ClassifyDAL()
        {
            _dbContext = new MyDbContext();
        }

        /// <summary>
        /// 查询分类
        /// </summary>
        /// <returns></returns>
        public Classify GetClassifyById(int id)
        {
            var query = _dbContext.Classifys.FirstOrDefault(x => x.Id == id);
            return query;
        }

        /// <summary>
        /// 查询所有分类
        /// </summary>
        /// <returns></returns>
        public List<Classify> GetClassify()
        {
            var query = _dbContext.Classifys;
            return query.ToList();
        }

        /// <summary>
        /// 添加或编辑分类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool EditClassify(Classify dto)
        {
            if (dto.Id > 0)
            {
                var model = _dbContext.Classifys.FirstOrDefault(x => x.Id == dto.Id);
                if (model == null) return false;
                model.Name = dto.Name;
            }
            else
            {
                var Classify = new Classify()
                {
                    Name = dto.Name,
                };
                _dbContext.Classifys.Add(Classify);
            }

            return _dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteClassify(int id)
        {       
            var model = _dbContext.Classifys.FirstOrDefault(x => x.Id == id);
            if (model == null) return false;

            //删除分类下的图书
            var books = _dbContext.Books.Where(x => x.ClassifyId == id).ToList();
            foreach(var book in books)
            {
                _dbContext.Books.Remove(book);
            }
            //删除分类
            _dbContext.Classifys.Remove(model);

            return _dbContext.SaveChanges() > 0;
        }
    }
}
