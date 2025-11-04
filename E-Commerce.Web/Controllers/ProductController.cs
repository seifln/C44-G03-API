using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet(template: "{id}")]
        public ActionResult<Product> GetById(int id)
        {
            if(id == 1)
            {
                return NotFound(); 
            }
            return new Product() { Id = id, Name = "Test" };
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll(string name)
        {
            return new List<Product>();
        }

        [HttpPost]
        // POST : BaseUrl/api/Product
        public ActionResult<Product> AddProduct(Product item)
        {
            return item;
        }

        [HttpPut]
        // PUT : BaseUrl/api/Product
        public ActionResult<Product> UpdateProduct(Product item)
        {
            return item;
        }

        [HttpDelete]
        public ActionResult<Product> DeleteProduct(Product item)
        {
            return item;
        }
    }
}
