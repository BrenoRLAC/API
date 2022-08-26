using API.Model;
using API.Service;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API.Interface
{
    public interface IHeroDao
    {
        Task<List<HeroModel>> ListHero();
        Task<HeroModel> ListHeroById(int id);

        List<HeroService> AddHeroes(HeroModel hero);
    }
}
