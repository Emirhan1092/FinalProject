﻿using Core.DataAccess.EntitiyFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Concrete.Entityframework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NorhwindContext>, ICategoryDal
    {
      
    }
}
