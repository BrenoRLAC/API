using API.Domain.Hero;

namespace API.Infrastructure.Interface
{
    public interface IHeroDao
    {
        Task<List<Hero>> ListHero();
        Task<Hero> ListHeroById(int id);
        Task AddHero(Hero hero);
        Task UpdateHero(Hero hero);
        Task DeleteHero(int id);
    }
}
