using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestProje.Interface;
using TestProje.Models;

namespace TestProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase, IFunctions<Location>
    {
        public readonly ApplicationDbContext Context;
        private const int LocationPerPage = 2;
        public LocationController(ApplicationDbContext Context)
        {
            this.Context = Context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllAsync()
        {
            try
            {
                var locations = await Context.Locations
                    .Include(l => l.Residents)
                    .ToListAsync();

                var totalLocation = Context.Locations.Count();
                var totalPages = (int)Math.Max(Math.Ceiling((double)totalLocation / LocationPerPage), 1);

                var results = locations.Select(loc => new
                {
                    Id = loc.Id,
                    Name = loc.Name,
                    Type = loc.LocType,
                    Dimension = loc.Dimension,
                    Residents = loc.Residents
                        .Select(resident => $"https://localhost:7087/api/characterapi/{resident.Id}")
                        .ToList(),
                    Url = $"https://localhost:7087/api/location/{loc.Id}",
                    Created = loc.Created
                }).ToList();

                var info = new Info
                {
                    Count = totalLocation,
                    Pages = totalPages,
                    Next = totalPages > 1 ? $"https://localhost:7087/api/location?page={results.First().Id+1}" : null,
                    Prev = totalPages > 2 ? $"https://localhost:7087/api/location?page={results.First().Id + -1}" : null,
                    //skip // take 
                };

                var response = new
                {
                    info,
                    results
                };

                return Ok(response);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetIdAsync(int id)
        {
            try
            {
                var location = await Context.Locations
                    .Include(l => l.Residents)
                    .FirstOrDefaultAsync(l => l.Id == id);

                if (location == null)
                {
                    return NotFound(); 
                }
                var result = new
                {
                    Id = location.Id,
                    Name = location.Name,
                    Type = location.LocType,
                    Dimension = location.Dimension,
                    Residents = location.Residents
                        .Select(resident => $"https://localhost:7087/api/characterapi/{resident.Id}")
                        .ToList(),
                    Url = $"https://localhost:7087/api/location/{location.Id}",
                    Created = location.Created
                };

                return Ok(result);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }
    }
}

