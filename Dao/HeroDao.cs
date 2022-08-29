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
    private SqlConnection _connection;

    private SqlConnection connection => _connection ??= new SqlConnection(_connectStr);

    public HeroDao(IConfiguration config)
    {
        _connectStr = config.GetConnectionString("Default");
    }
    public async Task<List<HeroModel>> ListHero() 
    {
        var hero = (await connection.QueryAsync<HeroModel>("listAllHeroes"),
                   commandType: CommandType.StoredProcedure);
        return (List<HeroModel>)hero.Item1;

    }
    public async Task<HeroModel> ListHeroById(int id) 
    {
            var hero = await connection.QueryFirstAsync<HeroModel>("ListHeroById",
            new
            {
                Id = id
            }, commandType: CommandType.StoredProcedure);
            return hero;                 
    }

    public async Task AddHeroes(HeroModel hero)
    {
        var connectionBd = "insertHero";
        var newHero = new DynamicParameters();
        newHero.Add("name", hero.Name);
        newHero.Add("FirstName", hero.FirstName);
        newHero.Add("LastName", hero.LastName);
        newHero.Add("Place", hero.Place);
        await connection.ExecuteAsync(connectionBd, newHero, commandType: CommandType.StoredProcedure);

    }
}


