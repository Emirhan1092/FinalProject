using Buisness.Abstract;
using Buisness.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.Entityframework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using NPoco.FluentMappings;
using System;
using System.Collections.Generic;
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

        public IResult Add(Product product)
        {
            if(product.ProductName.Length<2)
            {
                return new ErrorResult(Masseges.ProductNameInvalid);

            }
            _productDal.Add(product);

            return new SuccessResult(Masseges.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {

            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Product>>(Masseges.MaintenanceTime);
            }
            return new SuccesDataResult<List<Product>>(Masseges.ProductListed);

  
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
