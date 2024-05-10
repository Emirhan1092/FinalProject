using Buisness.Abstract;
using Buisness.Constants;
using Buisness.ValidationRules.FluentValidation;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Buisness;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Ajax.Utilities;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            this._productDal = productDal;
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 13)
            {
                return new ErrorDataResult<List<Product>>(Masseges.MaintenanceTime);
            }
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(), Masseges.ProductsListed);
        }

        //[SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfSameName(product.ProductName)
                , CheckIfProductCountOfCategoryCorrect(product.CategoryId));
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new Result(true, Masseges.ProductAdded);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal unitMin, decimal unitMax)
        {
            return new SuccesDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice <= unitMax && p.UnitPrice >= unitMin));
        }

        public IDataResult<List<ProductDetailsDto>> GetProductDetails()
        {
            return new SuccesDataResult<List<ProductDetailsDto>>(_productDal.GetProductDetails());
        }


        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccesDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult();
        }
        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.ProductId == categoryId);
            if (result.Count >= 10)
            {
                return new ErrorResult("Bir kategoride 10 adetten fazla ürün bulunamaz.");
            }
            return new SuccessResult();
        }
        private IResult CheckIfSameName(string name)
        {
            var result = _productDal.Get(p => p.ProductName == name);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [TransactionScopeAspect]
        public IResult AddTansactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice < 10) ;
            {
                throw new Exception("");

            }

            Add(product);
            return null;
        }
    }
}