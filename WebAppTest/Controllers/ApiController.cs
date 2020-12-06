using EFCoreMultiSchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTest.Database;
using WebAppTest.Models;

namespace WebAppTest.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {        
        [HttpPost("/environments")]
        public async Task<IActionResult> CreateAccount(
            [FromServices] AppDbContextManager dbContextManager)
        {
            var environmentId = AppDbContext.SchemaPrefix + DateTime.UtcNow.Ticks;
            await dbContextManager.CreateEnviroment(environmentId);

            return Ok(new { EnvironmentId = environmentId });
        }

        [HttpGet("/environments")]
        public async Task<IActionResult> GetEnvironments([FromServices] AppDbContextManager dbContextManager)
        {
            var environments = await dbContextManager.GetEnvironments();
            return Ok(environments);
        }

        [HttpGet("/contacts")]
        public async Task<IActionResult> GetContacts([FromServices] AppDbContext dbContext)
        {
           var contacts = await dbContext.Contacts.AsNoTracking().ToListAsync();
            return Ok(contacts);
        }

        [HttpPost("/contacts")]
        public async Task<IActionResult> CreateContact([FromBody]ContactModel contact,
            [FromServices] AppDbContext dbContext)
        {
            dbContext.Contacts.Add(new Database.Entities.Contact 
            {
                Name = contact.Name,
                Phone = contact.Phone
            });

            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
