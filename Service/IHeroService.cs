using API.Controllers;
using API.Model;

namespace API.Service
{
    public interface IHeroService
    {
        Task <HeroService> ListHero(int id);
        Task <List<HeroService>> AddHeroes(IHeroService hero);
    }
}
