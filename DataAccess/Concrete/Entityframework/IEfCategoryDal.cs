using Entities.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Concrete.Entityframework
{
    public interface IEfCategoryDal
    {
        void Add(Category entity);
        void Delete(Category entity);
        Category Get(Expression<Func<Category, bool>> filter);
        List<Category> GetAll(Expression<Func<Category, bool>> filter = null);
        void Update(Category entity);
    }
}