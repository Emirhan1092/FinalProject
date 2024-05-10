using Buisness.Abstract;
using Business.Concrete;
using DataAccess.Concrete.Entityframework;

namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProductTest();
            //CategoryTest();

        }

        private static void CategoryTest()
        {
            ICategoryService categoryManager = new CategoryManager(new EfCategoryDal());
            var category = categoryManager.GetById(2);
            Console.WriteLine(category.Data);
        }

        private static void ProductTest()
        {
            IProductService productManager = new ProductManager(new EfProductDal());

            //productManager.Add(new Product { ProductId = 6, CategoryId = 2, ProductName = "Mouse Pad", UnitInStock = 35, UnitPrice = 70 });

            foreach (var product in productManager.GetProductDetails().Data)
            {
                Console.WriteLine(product.ProductName + " " + product.CategoryName );
            }
        }
    }
}