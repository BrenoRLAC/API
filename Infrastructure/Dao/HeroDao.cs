using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using API.Infrastructure.Interface;
using API.Domain.Hero;

namespace API.Infrastructure.Dao;

public class HeroDao : IHeroDao
{
    private readonly string _connectStr;
    private SqlConnection _connection;

    private SqlConnection connection => _connection ??= new SqlConnection(_connectStr);

    public HeroDao(IConfiguration config)
    {
        _connectStr = config.GetConnectionString("Default");
    }
    public async Task<List<Hero>> ListHero()
    {
        var hero = (await connection.QueryAsync<Hero>("listAllHeroes"),
                   commandType: CommandType.StoredProcedure);
        return (List<Hero>)hero.Item1.AsList();

    }
    public async Task<Hero> ListHeroById(int id)
    {
        var hero = await connection.QueryFirstAsync<Hero>("ListHeroById",
        new
        {
            Id = id
        }, commandType: CommandType.StoredProcedure);
        return hero;
    }

    public async Task AddHero(Hero hero)
    {
        var connectionBd = "insertHero";
        var newHero = new DynamicParameters();
        newHero.Add("name", hero.Name);
        newHero.Add("DisguiseName", hero.DisguiseName);        
        newHero.Add("Place", hero.Place);
        await connection.ExecuteAsync(connectionBd, newHero, commandType: CommandType.StoredProcedure);

    }

    public async Task UpdateHero(Hero hero)
    {
        await connection.ExecuteAsync("updateHero", new
        {
            hero.Id,
            hero.Name,           
            hero.DisguiseName,
            hero.Place


        }, commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteHero(int id)
    {

        await connection.ExecuteAsync("deleteHero", new
        {
            id

        }, commandType: CommandType.StoredProcedure);
    }
}


