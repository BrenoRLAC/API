using API.Service;

namespace API.Interface
{
    public interface IHeroDao
    {
        Task Services(IHeroService hero);
        Task<HeroService> Services(int id);
    }
}
