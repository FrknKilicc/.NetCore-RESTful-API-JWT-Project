using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Cryptography.X509Certificates;
using TestProje.Interface;
using TestProje.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace TestProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterAPIController : ControllerBase, IFunctions<Character>
    {
        public readonly ApplicationDbContext Context;
        public CharacterAPIController(ApplicationDbContext Context)
        {
            this.Context = Context;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetAllAsync()
        {
            try
            {
                var result = from chr in Context.Characters join org in Context.Origins on chr.OriginId equals org.OriginId
                             select new
                             {
                                 chr.Id,
                                 chr.Name,
                                 chr.Status,
                                 chr.Species,
                                 chr.Type,
                                 chr.Gender,
                                 Origin = chr.Origin,
                                 Location = chr.location,
                                 Episodes = chr.CharacterEpisodes.Select(x => x.Episode).Select(x => x.Url).ToList(),
                                 chr.ImageUrl,
                                 chr.CreatedAt,
                             };
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetIdAsync(int id)
        {
            
            try
            {
                var result = await (from chr in Context.Characters.Where(x => x.Id == id)
                                    select new 
                                    {
                                        Id = chr.Id,
                                        Name = chr.Name,
                                        Status = chr.Status,
                                        Species = chr.Species,
                                        Type = chr.Type,
                                        Gender = chr.Gender,
                                        Origin = chr.Origin,
                                        location = chr.location,
                                        Episodes = chr.CharacterEpisodes.Select(x => x.Episode.Url).ToList(),
                                        ImageUrl = chr.ImageUrl,
                                        CreatedAt = chr.CreatedAt,
                                    }).FirstOrDefaultAsync();

                if (result == null)
                {
                    return NotFound(); 
                }

                return Ok(result); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}

