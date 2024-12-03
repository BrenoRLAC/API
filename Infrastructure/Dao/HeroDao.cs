using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using API.Infrastructure.Interface;
using API.Domain.Hero;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using API.Domain.HeroImages;
using API.Domain.Hero.AddressRequest;
using API.Utilities;
using API.Domain.Hero.AddressResults;
namespace API.Infrastructure.Dao;

public class HeroDao : IHeroDao
{
    private readonly string _connectStr;
    private SqlConnection _connection;
    private SqlConnection Connection => _connection ??= new SqlConnection(_connectStr);

    public HeroDao(IConfiguration config)
    {
        _connectStr = config.GetConnectionString("Default");
    }
    public async Task<List<HeroesResult>> ListHero()
    {

        var hero = await Connection.QueryAsync<HeroesResult>("SP_LS_ALL_HEROES", commandType: CommandType.StoredProcedure);


        foreach (var item in hero)
        {
            item.Id.EncryptInt();

            if (!string.IsNullOrEmpty(item.heroImage))
                item.heroImages = JsonConvert.DeserializeObject<List<HeroImage>>(item.heroImage);

        }

        return hero.ToList();
    }



    public async Task<HeroResult> GetHeroById(string id)
    {

        var hero = await Connection.QueryFirstOrDefaultAsync<HeroResult>("LIST_HERO_BY_ID", new { Id = id.DecryptInt() }, commandType: CommandType.StoredProcedure);


        if (hero != null) hero.Id.EncryptInt();
        if (hero.HeroImage == null) return hero;
        hero.HeroImages = JsonConvert.DeserializeObject<List<HeroImage>>(hero.HeroImage);


        return hero;

    }

    public async Task<AddressResult> GetHeroAddress(string heroId)
    {
        return await Connection.QueryFirstOrDefaultAsync<AddressResult>("SP_LS_HERO_ADDRESS",
        new { HERO_ID = heroId.DecryptInt() }, commandType: CommandType.StoredProcedure);


    }

    public async Task SetHero(HeroRequest hero)
    {

        await Connection.ExecuteAsync("INSERT_HERO", new
        {
            hero.Name,
            hero.DisguiseName,
            hero.Description,

        }, commandType: CommandType.StoredProcedure);

    }

    public async Task SetImage(string heroId, List<ImageUploadResult> image)
    {        
        var images = new DataTable("TP_CODE");

        images.Columns.Add("PUBLIC_ID", typeof(string));
        images.Columns.Add("URL", typeof(string));

        image?.ForEach(x => images.Rows.Add(x.PublicId, x.SecureUrl));


        await Connection.QueryFirstOrDefaultAsync<object>("INSERT_HERO_IMAGE", new
        {
            HERO_ID = heroId.DecryptInt(),
            IMAGES = images

        }, commandType: CommandType.StoredProcedure);

    }

    public async Task SetHeroAddress(string id, AddressRequest address)
    {

        await Connection.ExecuteAsync("INSERT_HERO_ADDRESS_AND_COMPLEMENT", new
        {
            HERO_ID = id.DecryptInt(),
            address.State,
            address.City,
            address.Neighborhood,
            address.Cep,
            address.Street,
            address.Country,
            address.Number,
            address.Complement,
            address.ReferencePoint
        }, commandType: CommandType.StoredProcedure);


    }

    public async Task UpdateHero(string heroId, HeroRequest hero)
    {
        await Connection.ExecuteAsync("UPDATE_HERO", new
        {
            heroId,
            hero.Name,
            hero.DisguiseName,
            hero.Description


        }, commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteHero(string id)
    {
        await Connection.ExecuteAsync("DELETE_HERO", new
        {
            id

        }, commandType: CommandType.StoredProcedure);
    }

    public async Task<string> ValidateHero(HeroRequest hero)
    {

        var id = await Connection.QueryFirstOrDefaultAsync<int>("VALIDATE_HERO", new
        {
            hero.Name,
            hero.DisguiseName,
            hero.Description


        }, commandType: CommandType.StoredProcedure);

        return id.EncryptInt();
    }
}

