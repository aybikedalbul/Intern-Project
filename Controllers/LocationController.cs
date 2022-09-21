using InternProject.Data;
using InternProject.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace InternProject.Controllers

{

    [EnableCors("test")]


    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly Context dbcontext;

        public LocationController(Context dbcontext)
        {
            this.dbcontext = dbcontext;
        }



        [HttpGet]

        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbcontext.Locations.ToListAsync());

        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetContact([FromRoute] int id)
        {
            var contact = dbcontext.Locations.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }


        [HttpPost]
        public async Task<IActionResult> AddContacts(LocationRequest requests)
        {

            var _location = new Location()
            {
                id = requests.id,
                name = requests.name,
                x = requests.x,
                y = requests.y,
            };

            await dbcontext.Locations.AddAsync(_location);
            await dbcontext.SaveChangesAsync();

            return Ok(_location);

        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateContact([FromRoute] int id, LocationUpdate updates)
        {
            var contact = dbcontext.Locations.Find(id);

            if (contact != null)
            {

                contact.name = updates.name;
                contact.x = updates.x;
                contact.y = updates.y;

                await dbcontext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();

        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {

            var contact = await dbcontext.Locations.FindAsync(id);

            if(contact != null)
            {
                dbcontext.Remove(contact);
                await dbcontext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }


    }
}