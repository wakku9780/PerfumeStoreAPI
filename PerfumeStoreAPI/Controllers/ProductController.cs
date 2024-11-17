using Microsoft.AspNetCore.Mvc;
using PerfumeStoreAPI.DTOs;
using PerfumeStoreAPI.Models;
using PerfumeStoreAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/Product
        //[HttpPost]
        //public async Task<ActionResult<Product>> CreateProduct(Product product)
        //{
        //    if (product.CategoryId <= 0)
        //    {
        //        return BadRequest("CategoryId is required");
        //    }

        //    await _productService.CreateProductAsync(product);
        //    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        //}


        //[HttpPost]
        //public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        //{
        //    // CategoryId ki validation
        //    if (product.CategoryId <= 0)
        //    {
        //        return BadRequest("Valid CategoryId is required.");
        //    }

        //    // Category ko validate karne ka check
        //    await _productService.CreateProductAsync(product);



        //    return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        //}


        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDto productDto)
        {
            if (productDto.CategoryId <= 0)
            {
                return BadRequest("Valid CategoryId is required.");
            }

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId,
                Brand = productDto.Brand,
                Size = productDto.Size,
                Gender = productDto.Gender,
                FragranceType = productDto.FragranceType,
                LaunchDate = productDto.LaunchDate,
                Rating = productDto.Rating
            };

            await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }




        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
