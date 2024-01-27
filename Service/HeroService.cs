using API.Domain.Hero;
using API.Infrastructure.Interface;

namespace API.Service
{
    public class HeroService : IHeroService
    {
        private readonly IHeroDao _dao;

        public HeroService(IHeroDao dao)
        {
            _dao = dao;
        }
        public Task<List<Hero>> ListHero()
        {
            return _dao.ListHero();
        }
        public Task<Hero> ListHeroById(int id)
        {
            return _dao.ListHeroById(id);
        }
        public Task AddHero(Hero hero)
        {
            return _dao.AddHero(hero);
        }

        public async Task<Hero> UpdateHero(Hero hero)
        {
            var validateHero = await _dao.ListHeroById(hero.Id);         
                 await _dao.UpdateHero(hero);
           
         return validateHero;
        }
        public async Task<Hero> DeleteHero(int id)
        {
            var validateHero = await _dao.ListHeroById(id); 
                await _dao.DeleteHero(id);
            return validateHero;
        }
    }
}
