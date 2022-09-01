using API.Interface;
using API.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Service
{
    public class HeroService : IHeroService
    {
        private readonly IHeroDao _dao;

        public HeroService(IHeroDao dao)
        {
            _dao = dao;
        }
        public Task<List<HeroModel>> ListHero()
        {
            return _dao.ListHero();
        }
        public Task<HeroModel> ListHeroById(int id)
        {
            return _dao.ListHeroById(id);
        }
        public Task AddHero(HeroModel hero)
        {
            return _dao.AddHero(hero);
        }

        public async Task<HeroModel> UpdateHero(HeroModel hero)
        {
            var validateHero = await _dao.ListHeroById(hero.Id);         
                 await _dao.UpdateHero(hero);
           
         return validateHero;
        }
        public Task DeleteHero(int id)
        {
            return _dao.DeleteHero(id);
        }
    }
}
