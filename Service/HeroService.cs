using API.Interface;
using Microsoft.AspNetCore.Diagnostics;

namespace API.Service
{
    public class HeroService : IHeroService
    {
        private readonly IHeroDao _dao; 

        public HeroService(IHeroDao dao)
        {
            _dao = dao;
        }

        public async Task<List<HeroService>> AddHeroes(IHeroService hero)
        {
            throw new NotImplementedException();
        }

        public Task<HeroService> ListHero(int id)
        {
            return _dao.Services(id);
        }
    }
}
