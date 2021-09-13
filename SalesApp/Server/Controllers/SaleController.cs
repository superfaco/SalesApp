using Microsoft.AspNetCore.Mvc;
using SalesApp.Server.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SalesApp.Shared.Models;

namespace SalesApp.Server.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SaleController : ControllerBase
    {
        private SalesAppDbContext context;

        public SaleController(SalesAppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await context.Sales.Include(s => s.SaleDetails).ToListAsync<Sale>());
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Sale sale)
        {
            try
            {
                ICollection<SaleDetail> saleDetails = sale.SaleDetails;
                sale.SaleDetails = null;

                await context.Sales.AddAsync(sale);
                await context.SaveChangesAsync();

                foreach(SaleDetail sd in saleDetails)
                {
                    sd.SaleId = sale.Id;
                    await context.SaleDetails.AddAsync(sd);
                }
                await context.SaveChangesAsync();

                return Ok(sale);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Sale sale)
        {
            try
            {
                ICollection<SaleDetail> saleDetails = sale.SaleDetails;
                sale.SaleDetails = null;
                context.Entry<Sale>(sale).State = EntityState.Modified;
                await context.SaveChangesAsync();

                List<SaleDetail> saleDetailsInDB = await context.SaleDetails.Where(sd => sd.SaleId == sale.Id).ToListAsync<SaleDetail>();
                if(saleDetailsInDB != null)
                {
                    foreach(SaleDetail sd in saleDetailsInDB)
                    {
                        context.SaleDetails.Remove(sd);
                    }
                    await context.SaveChangesAsync();
                }

                if(saleDetails != null)
                {
                    foreach(SaleDetail sd in saleDetails)
                    {
                        sd.SaleId = sale.Id;
                        sd.Id = null;
                        await context.SaleDetails.AddAsync(sd);
                    }
                    await context.SaveChangesAsync();
                }

                return Ok(sale);
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
                List<SaleDetail> saleDetails = await context.SaleDetails.Where(sd => sd.SaleId == id).ToListAsync<SaleDetail>();
                if(saleDetails != null)
                {
                    foreach(SaleDetail sd in saleDetails)
                    {
                        context.SaleDetails.Remove(sd);
                    }
                    await context.SaveChangesAsync();
                }


                Sale sale = await context.Sales.FirstOrDefaultAsync<Sale>(s => s.Id == id);
                if(sale != default)
                {
                    context.Sales.Remove(sale);
                    await context.SaveChangesAsync();
                    return Ok(sale);
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
