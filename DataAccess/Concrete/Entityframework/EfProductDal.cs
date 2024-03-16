using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DataAccess.Concrete.Entityframework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            using (NorhwindContext context = new NorhwindContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges(); 
            }
        }

        public void Delete(Product entity)
        {
            using (NorhwindContext context = new NorhwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorhwindContext context = new NorhwindContext())
            {

                return context.Set  <Product>().SingleOrDefault(filter);
            }
        }
        List<Product> IEntityRepository<Product>.GetAll(Expression<Func<Product, bool>> filter)
        {
           
        
            using (NorhwindContext context = new NorhwindContext())
            {

                return filter == null 
                    ? context.Set<Product>().ToList() 
                    : context.Set<Product>().Where(filter).ToList();
            }
      
        }
     

        public void Update(Product entity)
        {
            using (NorhwindContext context = new NorhwindContext())
            {
                var updatedEntity = context.Entry(entity);
                 updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

      
    }
}
