using API.Model;


namespace API.Infrastructure.Interface
{
    public interface IHeroDao
    {
        Task<List<HeroModel>> ListHero();
        Task<HeroModel> ListHeroById(int id);
        Task AddHero(HeroModel hero);
        Task UpdateHero(HeroModel hero);
        Task DeleteHero(int id);
    }
}
