using API.Infrastructure.Dao;
using API.Infrastructure.Interface;
using API.Jobs;
using API.Middleware;
using API.Service;
using Hangfire;
using RabbitMQ.Client;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IHeroDao, HeroDao>();
builder.Services.AddTransient<IHeroService, HeroService>();
builder.Services.AddTransient<IRedisUpdate, RedisUpdate>();
builder.Services.AddTransient<IRedisDao, RedisDao>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddHangfireServer();

ConnectionFactory factory = new();

factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Sender";

IConnection cnn = factory.CreateConnection();
IModel channel = cnn.CreateModel();

string exchangeName = "ExchangeQueue";
string routingKey = "routing-key";
string queue = "Queue";


channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queue, false, false, false, null);
channel.QueueBind(queue, exchangeName, routingKey, null);

byte[] messageBodyBytes = Encoding.UTF8.GetBytes("Hello World");
channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);

channel.Close();
cnn.Close();

builder.Services.AddSingleton<ConnectionMultiplexer>(provider =>
{
    var redisConnection = builder.Configuration["Redis:connection"];
    var configuration = ConfigurationOptions.Parse(redisConnection);
    return ConnectionMultiplexer.Connect(configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

var cronExpression = builder.Configuration["Intervals:IHeroesUpdate"];


recurringJobManager.AddOrUpdate<IRedisUpdate>("tempHeroes",
    x => x.Run(null), 
    cronExpression);


app.UseHangfireDashboard("/hangfire");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware();
app.MapControllers();
app.Run();
