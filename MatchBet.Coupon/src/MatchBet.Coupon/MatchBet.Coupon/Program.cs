using MatchBet.Coupon.Data;
using MatchBet.Coupon.Repositories.CouponRepository;
using MatchBet.Coupon.Repositories.MatchPredictRepository;
using MatchBet.Coupon.Services.CouponService;
using MatchBet.Coupon.Services.MatchPredictService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddTransient<ICouponRepository,CouponRepository>();
builder.Services.AddTransient<ICouponService,CouponService>();
builder.Services.AddTransient<IMatchPredictRepository,MatchPredictRepository>();
builder.Services.AddTransient<IMatchPredictService,MatchPredictService>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionString")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();
