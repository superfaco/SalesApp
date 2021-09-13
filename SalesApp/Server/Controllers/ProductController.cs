using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApp.Server.Contexts;
using SalesApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Server.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ProductController : ControllerBase
    {
        private SalesAppDbContext context;

        public ProductController(SalesAppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await context.Products.ToListAsync<Product>());
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            try
            {
                await context.Products.AddAsync(product);
                await context.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Product Product)
        {
            try
            {
                context.Entry<Product>(Product).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(Product);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                Product product = await context.Products.FirstOrDefaultAsync<Product>(p => p.Id == id);
                if(product != default)
                {
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                    return Ok(product);
                }
                return Problem();
            }
            catch (Exception)
            {
                return Problem();
            }
        }

    }
}
