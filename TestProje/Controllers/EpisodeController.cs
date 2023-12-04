using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json.Serialization;
using System.Text.Json;
using TestProje.Interface;
using TestProje.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;


namespace TestProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase, IFunctions<Episodes>
    {
        public readonly ApplicationDbContext Context;
        private const int EpisodessPerPage = 2;
        public EpisodeController(ApplicationDbContext Context)
        {
            this.Context = Context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Episodes>>> GetAllAsync()
        {
            Episodes episodes = new Episodes();
            try
            {
                
                var totalEpisodess = Context.Locations.Count();
                var totalPages = (int)Math.Max(Math.Ceiling((double)totalEpisodess / EpisodessPerPage), 1);
                var episodesWithCharacters = new
                {
                    info = new Info
                    {
                        Count = totalEpisodess,
                        Pages = totalPages, 
                        Next = episodes.Id >= 0 ? $"https://localhost:7087/api/episode?page={episodes.Id+1}" : null,
                        Prev = episodes.Id> 2 ? $"https://localhost:7087/api/episode?page={episodes.Id-1}" : null,
                    },
                    results = await (from Eps in Context.Episodess
                                     select new
                                     {
                                         Id = Eps.Id,
                                         Name = Eps.Name,
                                         AirDate = Eps.AirDate.ToString(),
                                         EpisodeCode = Eps.EpisodeCode,
                                         Characters = Eps.CharacterEpisodes 
                                            .Select(ce => $"https://localhost:7087/api/characterapi/{ce.Character.Id}")
                                            .ToList()
                                     }).ToListAsync()
                };
                return Ok(episodesWithCharacters);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Episodes>> GetIdAsync(int id)
        {
            try
            {
                var results = await (from Eps in Context.Episodess
                                     where Eps.CharacterEpisodes.Any(ce => ce.Character.Id == id)
                                     select new
                                     {
                                         Id = Eps.Id,
                                         Name = Eps.Name,
                                         AirDate = Eps.AirDate.ToString(),
                                         EpisodeCode = Eps.EpisodeCode,
                                         Characters = Eps.CharacterEpisodes
                                            .Select(ce => $"https://localhost:7087/api/characterapi{ce.Character.Id}")
                                            .ToList()
                                     }).ToListAsync();
              

                return Ok(results);
            }
            catch (Exception err)
            {
                return BadRequest(err);
            }
        }

    }
}
