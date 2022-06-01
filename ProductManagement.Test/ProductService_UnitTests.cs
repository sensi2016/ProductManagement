using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.Application.Interface;
using ProductManagement.DTO;
using ProductManagement.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductManagement.Test
{
    public class ProductService_UnitTests
    {
        private readonly Mock<IProductService> _productService;
        public ProductService_UnitTests()
        {
            _productService = new Mock<IProductService>();
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public async Task GetByIdTest()
        {
            //arrange
            int id = 1;
            var products = new BaseResponseDto() { Data = GetSampleProduct(), Status = ResponseStatus.Success };

            _productService.Setup(x => x.GetById(id))
                .ReturnsAsync(products);

            //act           
            var result = await _productService.Object.GetById(id);

            //assert           
            Assert.True(result.Status == ResponseStatus.Success);
            Assert.NotNull(result.Data);
        }

        [Fact]
        //naming convention MethodName_expectedBehavior_StateUnderTest
        public async Task AddProductTest()
        {
            //Arrange
            RequestProductDto productEntity = null;
            _productService.Setup(srvc => srvc.Add(It.IsAny<RequestProductDto>())).Callback<RequestProductDto>(x => productEntity = x);
            var productData = new RequestProductDto
            {
                Title="benz",
                Code="123",
                Count=10,
                IsActive=true,
                Price=1000,
                ProductTypeId=1
            };
            //Act
            await _productService.Object.Add(productData);
            //Assert
            _productService.Verify(x => x.Add(It.IsAny<RequestProductDto>()), Times.Once);

            Assert.Equal(productEntity.Title, productData.Title);
            Assert.Equal(productEntity.Count, productData.Count);
            Assert.Equal(productEntity.Price, productData.Price);
            Assert.Equal(productEntity.ProductTypeId, productData.ProductTypeId);
            Assert.Equal(productEntity.IsActive, productData.IsActive);
        }

        [Fact]
        public async Task UpdateProductTest()
        {
            //Arrange
            RequestProductDto productEntity = null;
            _productService.Setup(srvc => srvc.Update(It.IsAny<RequestProductDto>())).Callback<RequestProductDto>(x => productEntity = x);
            var productData = new RequestProductDto
            {
                Title = "benz update",
                Code = "1235",
                Count = 11,
                IsActive = true,
                Price = 1000,
                ProductTypeId = 1
            };
            //Act
            await _productService.Object.Update(productData);
            //Assert
            _productService.Verify(x => x.Update(It.IsAny<RequestProductDto>()), Times.Once);

            Assert.Equal(productEntity.Title, productData.Title);
            Assert.Equal(productEntity.Count, productData.Count);
            Assert.Equal(productEntity.Price, productData.Price);
            Assert.Equal(productEntity.ProductTypeId, productData.ProductTypeId);
            Assert.Equal(productEntity.IsActive, productData.IsActive);
        }

        [Fact]
        public async Task DeleteProduct()
        {
            //Arrange
            var id = 2;
            _productService.Setup(srvc => srvc.Delete(id));
            //Act
            await _productService.Object.Delete(id);
            //Assert
            _productService.Verify(repo => repo.Delete(id), Times.Once);
        }

        public List<ResponseProductDto> GetSampleProduct()
        {
            List<ResponseProductDto> output = new List<ResponseProductDto>
            {
                new ResponseProductDto
                {
                    Id = 1,
                    Title = "benz",
                    IsActive = true,
                    Price = 1000,
                    Count = 100,
                    ProductTypeId = 1
                },
                new ResponseProductDto
                {
                    Id = 2,
                    Title = "porsche",
                    IsActive = true,
                    Price = 2000,
                    Count = 100,
                    ProductTypeId = 1
                }
            };

            return output;
        }
    }
}