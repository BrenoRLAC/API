using API.Controllers;
using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Service
{
    public interface IHeroService
    {
        Task<List<HeroModel>> ListHero();
        Task<HeroModel> ListHeroById(int id);
        Task AddHero(HeroModel hero);
        Task<HeroModel> UpdateHero(HeroModel hero);
        Task<HeroModel> DeleteHero(int id);

    }
}
