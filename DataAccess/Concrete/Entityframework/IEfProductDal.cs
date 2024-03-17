using DataAccess.Abstract;
using Entities.DTOs;

namespace DataAccess.Concrete.Entityframework
{
    public interface IEfProductDal
    {
        List<ProductDetailsDto> GetProductDetails();
    }
}