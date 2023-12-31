using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
       
        public ProductsController(IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> productBrandRepo,IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>>  GetProducts()
        {
            var spec=new ProductWithTypesAndBrandSpecification();
            var products= await _productsRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
            // return products.Select(product=> new ProductToReturnDto
            // {
            //     Id=product.Id,
            //     Name=product.Name,
            //     Description=product.Description,
            //     PictureUrl=product.PictureUrl,
            //     Price=product.Price,
            //     ProductBrand=product.ProductBrand.Name,
            //     ProductType=product.ProductType.Name
            // }).ToList();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec=new ProductWithTypesAndBrandSpecification(id);
            var product= await _productsRepo.GetEntityWithSpec(spec);
            if(product==null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductToReturnDto>(product);
         
        }
         [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>  GetProductTypes()
        {
            var productTypes= await _productTypeRepo.ListAllAsync();
            return Ok(productTypes);
        }
         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>  GetProductBrands()
        {
            var productBrands= await _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
        }
    }
}