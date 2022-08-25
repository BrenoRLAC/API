using API.Model;
using API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class API : Controller
    {
        private readonly IHeroService _heroService;  
        public API(IHeroService heroService)
        {
            _heroService = heroService;
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHero(int id)
        {
           
            await _heroService.ListHero(id);
            return Ok();


        }

        [HttpPost]
        public async Task<ActionResult<IHeroService>> AddHero(IHeroService hero)
        {
            await _heroService.AddHeroes(hero);
            return Ok(hero);
        }

    }
}
