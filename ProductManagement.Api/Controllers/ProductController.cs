using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Api.Infrastructure;
using ProductManagement.Application.Interface;
using ProductManagement.DTO.Product;

namespace ProductManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new CustomActionResult(await _productService.GetById(id));
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(FilterProductDto dto)
        {
            return new CustomActionResult(await _productService.Search(dto));
        }

        [HttpPost()]
        public async Task<IActionResult> Add(RequestProductDto dto)
        {
            return new CustomActionResult(await _productService.Add(dto));
        }

        [HttpPut()]
        public async Task<IActionResult> Update(RequestProductDto dto)
        {
            return new CustomActionResult(await _productService.Update(dto));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return new CustomActionResult(await _productService.Delete(id));
        }
    }
}
