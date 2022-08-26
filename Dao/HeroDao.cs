using API.Interface;
using API.Service;
using Dapper;
using Npgsql;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Util;

public class HeroDao : IHeroDao
{
    private readonly string _connectStr;
    private NpgsqlConnection _connection;

    private NpgsqlConnection connection => _connection ??= new NpgsqlConnection(_connectStr);

    public HeroDao(IConfiguration config)
    {
        _connectStr = config.GetConnectionString("Default");
    }
    public async Task<List<HeroModel>> ListHero() 
    {
        var hero = (await connection.QueryAsync<HeroModel>("StoredProcedureName"),
                   commandType: CommandType.StoredProcedure);
        return (List<HeroModel>)hero.Item1;

    }
    public async Task<HeroModel> ListHeroById(int id)
    {
        var hero = await connection.QueryFirstAsync<HeroModel>("Select * from api.hero a where id = @Id",
            new { Id = id });
        return hero;
    }

    public List<HeroService> AddHeroes(HeroModel hero)
    {
        throw new NotImplementedException();
    }
}


