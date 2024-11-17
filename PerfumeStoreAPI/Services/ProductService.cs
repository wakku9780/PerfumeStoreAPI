using PerfumeStoreAPI.Models;
using PerfumeStoreAPI.Repositories;
using System.Threading.Tasks;

namespace PerfumeStoreAPI.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository; // Assuming you have a Category Repository

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        //public async Task CreateProductAsync(Product product)
        //{
        //    // Category valid hai ya nahi check karna
        //    var category = await _categoryRepository.GetCategoryByIdAsync(product.CategoryId);
        //    if (category == null)
        //    {
        //        throw new KeyNotFoundException("Category not found");
        //    }

        //    // Category ko product se link karna
        //    product.Category = category;

        //    // Product save karna
        //    await _productRepository.CreateProductAsync(product);
        //}

        public async Task CreateProductAsync(Product product)
        {
            Console.WriteLine($"Product Name: {product.Name}, CategoryId: {product.CategoryId}");

            var category = await _categoryRepository.GetCategoryByIdAsync(product.CategoryId);
            if (category == null)
            {
                Console.WriteLine($"Category not found for Id: {product.CategoryId}");
                throw new KeyNotFoundException("Category not found");
            }

            product.Category = category;
            await _productRepository.CreateProductAsync(product);
        }



        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }
}
