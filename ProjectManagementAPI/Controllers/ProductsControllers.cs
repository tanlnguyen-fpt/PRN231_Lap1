using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsControllers : ControllerBase
    {
        private IProductRepository productRepository = new ProductRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return Ok(productRepository.GetProducts());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            try
            {
                productRepository.SaveProduct(product);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                productRepository.DeleteProduct(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut()]
        public IActionResult Update(Product product)
        {
            try
            {
                productRepository.UpdateProduct(product);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            try
            {
                return Ok(productRepository.GetProductById(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
