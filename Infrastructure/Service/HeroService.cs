using API.Domain.Hero;
using API.Domain.Hero.Addresses;
using API.Domain.Hero.AddressRequest;
using API.Domain.Hero.AddressResults;
using API.Domain.HeroImages;
using API.Infrastructure.Interface;
using API.Utilities;
using CloudinaryDotNet.Actions;

namespace API.Infrastructure.Service
{
    public class HeroService : IHeroService
    {
        private readonly IHeroDao _dao;

        public HeroService(IHeroDao dao)
        {
            _dao = dao;
        }
        public Task<List<HeroesResult>> ListHero()
        {
            return _dao.ListHero();
        }
        public Task<HeroResult> GetHeroById(string id)
        {
            return _dao.GetHeroById(id);
        }

        public async Task SetHero(HeroRequest hero)
        {

            var heroData = await _dao.ValidateHero(hero);
            
            if (heroData != 0.EncryptInt())
            {               
                throw new ArgumentException("Hero already exists.");
            }

           await _dao.SetHero(hero);

        }

        public Task SetImage(string heroId, List<ImageUploadResult> image)
        {
            return _dao.SetImage(heroId, image);
        }

        public Task SetHeroAddress(string id, AddressRequest address)
        {
            return _dao.SetHeroAddress(id, address);
        }
        public async Task UpdateHero(string heroId, HeroRequest hero)
        {

            await _dao.UpdateHero(heroId, hero);

        }
        public async Task DeleteHero(string id)
        {
            await _dao.DeleteHero(id);

        }
        public Task<AddressResult> GetHeroAddress(string heroId)
        {
            return _dao.GetHeroAddress(heroId);
        }

        //public Task<HeroResult> ValidateHero(HeroRequest hero)
        //{
        //    return _dao.ValidateHero(hero);
        //}
    }
}
