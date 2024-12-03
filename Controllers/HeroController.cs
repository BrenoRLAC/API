using API.Domain;
using API.Domain.Hero;
using API.Domain.Hero.AddressRequest;
using API.Infrastructure.Interface;
using API.Utilities;
using CloudinaryServiceInterface.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class Hero : ControllerBase
{
    private readonly IHeroService _heroService;
    private readonly IAddressService _addressService;
    private readonly ICloudinaryService _cloudinaryService;

    public Hero(IHeroService heroService, IAddressService addressService, ICloudinaryService cloudinaryService)
    {
        _heroService = heroService;
        _addressService = addressService;
        _cloudinaryService = cloudinaryService;
    }

    [HttpGet, Route("AllHeroes"), Consumes("application/json"), Produces("application/json", Type = typeof(List<ReturnApi<HeroResult>>))]
    public async Task<IActionResult> GetAllHeroes()
    {
        var result = await _heroService.ListHero();

        return Ok(new ReturnApi<List<HeroesResult>>(200, result));
    }


    [HttpGet("HeroById/{heroId}"), Consumes("application/json"), Produces("application/json", Type = typeof(ReturnApi<HeroResult>))]
    public async Task<IActionResult> GetHero(string heroId)
    {

        var hero = await _heroService.GetHeroById(heroId);

        if (hero is null)
            return BadRequest(new ReturnApi<HeroResult>(400, "This Hero does not exist"));


        var address = await _heroService.GetHeroAddress(heroId);

        hero.AddressResult = address;

        return Ok(new ReturnApi<HeroResult>(200, hero));

    }


    [HttpPost, Produces("application/json", Type = typeof(ReturnApi<object>))]
    public async Task<IActionResult> SetHero(HeroRequest hero)
    {
        try
        {
             await _heroService.SetHero(hero);
            
            return Ok(new ReturnApi<object>(201, "Hero added successfully"));

        }catch(Exception ex)
        {
            return BadRequest (new ReturnApi<object>(400, ex.Message));
        }


    }

    [HttpPost("HeroImage/{heroId}"), Produces("application/json", Type = typeof(ReturnApi<object>))]
    public async Task<IActionResult> SetHeroImage(string heroId, List<IFormFile> images)
    {

        try
        {
            if (images == null || images.Count == 0) return BadRequest(new ReturnApi<object>(400, "Add at least one image"));


            var image = _cloudinaryService.UploadImages(images);

            if (images == null)
                return BadRequest(new ReturnApi<HeroResult>(400, "There is a problem while uploading the image of the hero"));            

       
            var setImage = _heroService.SetImage(heroId, image);

        }
        catch (Exception ex)
        {
            return BadRequest(new ReturnApi<object>(400, ex.Message));
        }


        return Ok(new ReturnApi<object>(201, "Hero's image added successfully"));
    }

    [HttpPost, Route("HeroAddress"), Consumes("application/json"), Produces("application/json", Type = typeof(ReturnApi<object>))]
    public async Task<IActionResult> SetHeroAddress(string heroId, [FromBody] AddressRequest address)
    {

        var heroIdEncrypted = 2023.EncryptInt();

        heroId = heroIdEncrypted;

        var addressValidation = await _addressService.GetAddress(address.Cep);

        var errors = new List<string>();

        if (addressValidation == null)
            errors.Add("Invalid Address");

        if (errors.Any())
        {
            return BadRequest(string.Join("\n", errors));
        }

        await _heroService.SetHeroAddress(heroIdEncrypted, address);

        return Ok(new ReturnApi<object>(200, "Hero added successfully"));
    }


    [HttpPut("{id}")]
    [Produces("application/json", Type = typeof(ReturnApi<object>))]
    public async Task<IActionResult> UpdateHero(string id, HeroRequest hero)
    {

        await _heroService.UpdateHero(id, hero);
        return Ok(new ReturnApi<object>(200, "Hero updated successfully"));
    }

    [HttpDelete("{id}")]
    [Consumes("application/json")]
    [Produces("application/json", Type = typeof(ReturnApi<object>))]
    public async Task<IActionResult> DeleteHero([FromRoute] string id)
    {
        await _heroService.DeleteHero(id);
        return Ok(new ReturnApi<object>(200, "Hero deleted successfully"));
    }

}

