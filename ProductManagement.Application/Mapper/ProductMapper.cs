using ProductManagement.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Mapper
{
    public class ProductMapper
    {
        public static Product Map(RequestProductDto productDto, Product product = null)
        {
            Product oProduct = null;

            if (product == null)
            {
                oProduct = new Product(); 
                oProduct.CreateDate = DateTime.Now;
                oProduct.IsActive = true;
            }
            else
            {
                oProduct = product;
                oProduct.IsActive=productDto.IsActive;

            }

            oProduct.Title = productDto.Title;
            oProduct.Brand = productDto.Brand;
            oProduct.Code = productDto.Code;
            oProduct.Count = productDto.Count;
            oProduct.Price = productDto.Price;
            oProduct.ProductTypeId = productDto.ProductTypeId;
            oProduct.ModifyDate = DateTime.Now;

            return oProduct;
        }
        

        public static ResponseProductDto Map(Product product)
        {
            return new ResponseProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Brand = product.Brand,
                Code = product.Code,
                Count = product.Count,
                CreateDate = product.CreateDate.ToShortDateString(),
                IsActive = product.IsActive,
                ModifyDate = product.ModifyDate.ToShortDateString(),
                Price = product.Price,
                ProductTypeId = product.ProductTypeId,

            }; 
        }

        public static Expression<Func<Product,ResponseListProductDto>> MapList
        {
            get
            {
                return x => new ResponseListProductDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Brand = x.Brand,
                    Count = x.Count,
                    CreateDate = x.CreateDate.ToShortDateString(),
                    ModifyDate = x.ModifyDate.ToShortDateString(),
                    IsActive = x.IsActive,
                    Price = x.Price
                };
            }
        }

    }

}

