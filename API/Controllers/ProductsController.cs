using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
       
        private readonly IProductRepository _repo;
        
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
            
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>>  GetProducts()
        {
            var products= await _repo.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }
         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>  GetProductTypes()
        {
            var productTypes= await _repo.GetProductTypesAsync();
            return Ok(productTypes);
        }
         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>  GetProductBrands()
        {
            var productBrands= await _repo.GetProductBrandsAsync();
            return Ok(productBrands);
        }
    }
}