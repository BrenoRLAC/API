using API.Controllers;
using API.Domain.Hero;
using Microsoft.AspNetCore.Mvc;

namespace API.Service
{
    public interface IHeroService
    {
        Task<List<Hero>> ListHero();
        Task<Hero> ListHeroById(int id);
        Task AddHero(Hero hero);
        Task<Hero> UpdateHero(Hero hero);
        Task<Hero> DeleteHero(int id);

    }
}
