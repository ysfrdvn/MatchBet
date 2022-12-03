using MatchBet.Player.Data;
using MatchBet.Player.Repository;
using MatchBet.Player.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

void ConfigureServices(WebApplicationBuilder? webApplicationBuilder)
{
        
    webApplicationBuilder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    webApplicationBuilder.Services.AddEndpointsApiExplorer();
    webApplicationBuilder.Services.AddSwaggerGen();
    webApplicationBuilder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
    webApplicationBuilder.Services.AddTransient<IPlayerServices, PlayerServices>();
    webApplicationBuilder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
        opt.UseNpgsql(webApplicationBuilder.Configuration.GetValue<string>("ConnectionString")));
}