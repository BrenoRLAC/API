using API.Domain.Hero;
using API.Domain.Hero.Addresses;
using API.Domain.Hero.AddressRequest;
using API.Domain.Hero.AddressResults;
using API.Domain.HeroImages;
using CloudinaryDotNet.Actions;

namespace API.Infrastructure.Interface
{
    public interface IHeroService
    {
        Task<List<HeroesResult>> ListHero();
        Task<HeroResult> GetHeroById(string heroId);
        Task<AddressResult> GetHeroAddress(string heroId);       
        Task SetHero(HeroRequest hero);
        Task SetImage(string heroId, List<ImageUploadResult> image);
        Task SetHeroAddress(string id, AddressRequest address);
        Task UpdateHero(string heroId, HeroRequest hero);
        Task DeleteHero(string id);       
    }
}
