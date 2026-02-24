using Microsoft.AspNetCore.Mvc;
using UserManagement.Entities;
using UserManagement.Model.Response;

namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryManagementContext _context;
        public ProductCategoryController(ProductCategoryManagementContext context)
        {
            _context = context;
        }

        [HttpGet("Category/GetCategories")]
        public ActionResult<List<CategoryResponse>> GetCategory()
        {
            var categories = _context.Categories
                .Select(x => new CategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type
                })
                .ToList();
            return Ok(categories);
        }

        [HttpGet("Category/{id}/GetSubCategory")]
        public ActionResult<List<SubCategoryResponse>> GetSubCategory([FromRoute] int id)
        {
            var subCategories = _context.SubCategories.Where(x => x.CategoryType == id)
                .Select(x => new SubCategoryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.CategoryTypeNavigation.Name
                })
                .ToList();
            return Ok(subCategories);
        }

        [HttpGet("Category/{id}/GetProduct")]
        public ActionResult<List<ProductResponse>> GetProduct([FromRoute] int id)
        {
            var products = _context.Products.Where(x => x.SubCategory == id).
                Select(x => new ProductResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    SubCategory = x.SubCategoryNavigation.Name
                })
                .ToList();
            return Ok(products);
        }

        [HttpGet("GetUsers")]
        public ActionResult<string> GetUsers()
        {
            return "Login successful";
        }
    }
}
