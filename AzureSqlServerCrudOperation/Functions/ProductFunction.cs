using AzureSqlServerCrudOperation.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AzureSqlServerCrudOperation
{
    public class ProductFunction
    {
        private readonly AppDbContext _appDbContext;

        public ProductFunction(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [FunctionName("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequest req,
            ILogger log)
        {
            var products = await _appDbContext.Products.ToListAsync();

            return new OkObjectResult(products);
        }

        [FunctionName("GetProductById")]
        public async Task<IActionResult> GetProductById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{Id}")] HttpRequest req,
            ILogger log,int Id)
        {
            var product = await _appDbContext.Products.FindAsync(Id);

            return new OkObjectResult(product);
        }

        [FunctionName("SaveProduct")]
        public async Task<IActionResult> SaveProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "products")] HttpRequest req,
            ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(body);

            _appDbContext.Products.Add(product);

            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult(product);
        }

        [FunctionName("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "products")] HttpRequest req,
            ILogger log)
        {
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            var product = JsonConvert.DeserializeObject<Product>(body);

            _appDbContext.Products.Update(product);

            await _appDbContext.SaveChangesAsync();

            return new NoContentResult();
        }

        [FunctionName("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "products/{Id}")] HttpRequest req,
            ILogger log,int Id)
        {
            var product = await _appDbContext.Products.FindAsync(Id);

            _appDbContext.Products.Remove(product);

            await _appDbContext.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
