using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntitiyFramework;
using Entities.DTOs;

namespace DataAccess.Concrete.Entityframework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorhwindContext>, IProductDal
    {
        public List<ProductDetailsDto> GetProductDetails()
        {
            using (NorhwindContext context = new NorhwindContext())

            {

                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailsDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName ,
                                 UntisStock = p.UnitsInStock
                             };
                return result.ToList(); 

            }
        }
    }
}
