using API.Interface;
using API.Service;
using Dapper;
using Npgsql;
using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Util;

public class HeroDao : IHeroDao
{
    private readonly string _connectStr;
    private NpgsqlConnection _connection;

    private NpgsqlConnection Connection => _connection ??= new NpgsqlConnection(_connectStr);

    public HeroDao(IConfiguration config)
    {
        _connectStr = config.GetConnectionString("Default");
    }
    public async Task<List<HeroModel>> ListHero() 
    {
        var hero = await Connection.QueryAsync<HeroModel>("Select * from api.hero a");
        return hero.AsList().ToList(); 
       
    }
    public async Task<HeroModel> ListHeroById(int id)
    {
        var hero = await Connection.QueryFirstAsync<HeroModel>("Select * from api.hero a where id = @Id",
            new { Id = id });
        return hero;
    }
}


