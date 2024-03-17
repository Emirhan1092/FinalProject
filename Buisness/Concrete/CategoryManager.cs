using Buisness.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categorDal;

        public CategoryManager()
        {
        }

        public CategoryManager(ICategoryDal categorDal)
        {
            _categorDal = categorDal;
        }

        public List<Category> GetAll()
        {
            return _categorDal.GetAll();

        }

        public Category GetById(int categoryId)
        {
            return _categorDal.Get(c => c.CategoryId == categoryId);
        }

        
    }
}
