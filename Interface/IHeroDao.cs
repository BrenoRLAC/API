using API.Model;
using API.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Interface
{
    public interface IHeroDao
    {
        //Task Services(HeroModel hero);
        Task<List<HeroModel>> ListHero();
        Task<HeroModel> ListHeroById(int id);
        
    }
}
