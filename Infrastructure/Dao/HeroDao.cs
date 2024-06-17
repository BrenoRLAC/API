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
        var hero = await connection.QueryAsync<Hero>("SP_LS_ALL_HEROES", commandType: CommandType.StoredProcedure);
        return hero.AsList();
    }


    public async Task<Hero> ListHeroById(int id)
    {
        var hero = await connection.QueryFirstAsync<Hero>("LIST_HERO_BY_ID",
        new
        {
            Id = id
        }, commandType: CommandType.StoredProcedure);
        return hero;
    }

    public async Task AddHero(Hero hero)
    {       
            await connection.ExecuteAsync("INSERT_HERO", new
            {
                hero.Name,
                hero.DisguiseName,
                hero.Place

            }, commandType: CommandType.StoredProcedure);
        

    }

    public async Task UpdateHero(Hero hero)
    {
        await connection.ExecuteAsync("UPDATE_HERO", new
        {
            hero.Id,
            hero.Name,
            hero.DisguiseName,
            hero.Place

        }, commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteHero(int id)
    {
        await connection.ExecuteAsync("DELETE_HERO", new
        {
            id

        }, commandType: CommandType.StoredProcedure);
    }
}

