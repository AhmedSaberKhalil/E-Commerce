using E_CommerceWebApi.Controllers;
using E_CommerceWebApi.DTO.DtoModels;
using E_CommerceWebApi.Models;
using E_CommerceWebApi.Repository;
using E_CommerceWebApi.Service;
using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace E_CommerceWebApi.Tests
{
    public class ProductControllerTest
    {
      //  private readonly Mock<IRepository<Product>> productService;
        public ProductControllerTest()
        {
           
        }

        [Fact]
        public void GetAll_IfTheDataIsExist_ReturnProductList()
        {
            //arrange
            var productList = GetProductsData();
            var productService = A.Fake<IProductRepository>();
            A.CallTo(() => productService.GetAll()).Returns(productList);
         
            var productController = new ProductController(productService);

            //act
            var productResult = productController.GetAll();
            var productType = productResult as OkObjectResult;
            var Result = productType.Value;
            //assert
            Assert.NotNull(Result);
            Assert.True(productList.Equals(Result));
            Assert.Equal(GetProductsData().ToString(), Result.ToString());
         
        }
        //[Fact]
        //public void GetAll_ProductList()
        //{
        //    //arrange
        //    var productService = new Mock<IProductRepository>();
        //    var productList = GetProductsData();
        //    productService.Setup(x => x.GetAll())
        //        .Returns(productList);
        //    var productController = new ProductController(productService.Object);

        //    //act
        //    var productResult = productController.GetAll();
        //    var productResultType = productResult as OkObjectResult;
        //    var pro = productResultType.Value;

        //    //assert
        //    Assert.NotNull(pro);
        //    Assert.Equal(GetProductsData().ToString(), pro.ToString());
        //    Assert.True(productList.Equals(pro));
        //}

        [Fact]
        public void GetById_IfIdsIsValied_ReturnProductListOfThisId()
        {
            // Arrange
            var productList = GetProductsData();
            var productService = A.Fake<IProductRepository>();
            A.CallTo(() => productService.GetById(2)).Returns(productList[2]);
            var productController = new ProductController(productService);

            //act
            var productResult = productController.GetById(2);
            var productResultType = productResult as OkObjectResult;
            // var productResult2 = productController.GetAll();
            var pro = productResultType.Value as Product;
            //assert
            Assert.NotNull(productResult);
            Assert.Equal(productList[2].Id, pro.Id);

        }
        [Fact]
        public void Add_WhenModelIsValied_EditProduct()
        {
            var productDto = new ProductDto
            {
                Id = 9,
                Name = "IPhone",
                Description = "IPhone 12",
                Price = 55000
            };
            //arrange
            var productList = GetProductsData();
            var productService = A.Fake<IProductRepository>();
            A.CallTo(() => productService.Add(productList[1]));
            var productController = new ProductController(productService);

            //act
            var productResult = productController.AddProduct(productDto);

            //assert
            Assert.NotNull(productResult);

        }

        [Fact]
        public void Edit_WhenModelIsValied_EditProduct()
        {
            var productDto = new ProductDto
            {
                Id = 9,
                Name = "IPhone",
                Description = "IPhone 12",
                Price = 55000
            };
            //arrange
            var productList = GetProductsData();
            var productService = A.Fake<IProductRepository>();
            A.CallTo(() => productService.Update(9, productList[1]));
            var productController = new ProductController(productService);

            //act
            var productResult = productController.Edit(9, productDto);

            //assert
            Assert.NotNull(productResult);
           
        }
        [Fact]
        public void Delete_WhenIdIsValied_DeleteProductById()
        {
           
            //arrange
            var productList = GetProductsData();
            var productService = A.Fake<IProductRepository>();
            A.CallTo(() => productService.Delete(productList[0]));
            var productController = new ProductController(productService);

            //act
            var productResult = productController.Delete(1);

            //assert
            Assert.NotNull(productResult);

        }

        private List<Product> GetProductsData()
        {
            List<Product> productsData = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "IPhone",
                Description = "IPhone 12",
                Price = 55000
            },
             new Product
            {
                Id = 2,
                Name = "Laptop",
                Description = "HP Pavilion",
                Price = 100000
            },
             new Product
            {
                Id = 3,
                Name = "TV",
                Description = "Samsung Smart TV",
                Price = 35000
            },
        };
            return productsData;
        }
        
       
    }
}
