using API.Domain.Hero;
using API.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class API : ControllerBase
{
    private readonly IHeroService _heroService;
    public API(IHeroService heroService)
    {
        _heroService = heroService;

    }

    [HttpGet]
    public async Task<ActionResult<List<Hero>>> GetAllHeroes()
    {
        var result = await _heroService.ListHero();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetHero(int id)
    {

        var result = await _heroService.ListHeroById(id);
        return Ok(result);


    }

    [HttpPost]
    public async Task<IActionResult> AddHero(Hero hero)
    {
        await _heroService.AddHero(hero);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHero([FromRoute] int id, Hero hero)
    {
        hero.Id = id;
        await _heroService.UpdateHero(hero);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHero([FromRoute] int id)
    {
        await _heroService.DeleteHero(id);
        return Ok();
    }

}

