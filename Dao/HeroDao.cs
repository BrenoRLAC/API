using API.Interface;
using API.Service;
using Dapper;
using Npgsql;


namespace API.Util
{
    public class HeroDao : IHeroDao
    {
        private readonly string _connectStr;
        private NpgsqlConnection _connection;

        private NpgsqlConnection Connection => _connection ??= new NpgsqlConnection(_connectStr);

        public HeroDao(IConfiguration config)
        {
            _connectStr = config.GetConnectionString("Default");
        }
        public Task Services(IHeroService hero)
        {
            throw new NotImplementedException();
        }

        public async Task<HeroService> Services(int id)
        {
            var hero = await Connection.QueryFirstAsync<HeroService>("Select * from api.hero a where a.id = @Id",
                new { Id = id });
            return hero;
        }
    }
}

