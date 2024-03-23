using Buisness.Abstract;
using Buisness.Constants;
using Buisness.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buisness.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager()
        {
        }

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
             
         

            _productDal.Add(product);

            return new SuccessResult(Masseges.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {

            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<List<Product>>(Masseges.MaintenanceTime);
            }
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(),Masseges.ProductListed); ;

  
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id));
        }

        public IDataResult<Product> GetById(int productid)
        {
            return new SuccesDataResult<Product>(_productDal.Get(p=>p.ProductId==productid));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailsDto>> GetProductDetails()
        {
           
            return new SuccesDataResult<List<ProductDetailsDto>>(_productDal.GetProductDetails());

        }


    }
}
