using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Serilog;
using System.Text;
using Team1_CryptoCore.Application.Interface;
using Team1_CryptoCore.Application.Mapping;
using Team1_CryptoCore.Infrastructure.Data;
using Team1_CryptoCore.Infrastructure.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();



builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));


builder.Services.AddScoped<JwtIService, JwtService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddScoped<IWalletService, WalletService>();

builder.Services.AddScoped<IFavouriteCoinService, FavouriteCoinService>();

builder.Services.AddScoped<ICoinGeckoService, CoinGeckoService>();

builder.Services.AddScoped<ICoinGeckoMarketService, CoinMarketService>();

builder.Services.AddScoped<ICoinGeckoID, CoinGeckoIDService>();

// For Cron
builder.Services.AddScoped<ICronGeckoService, CronGeckoService>();

builder.Services.AddAutoMapper(typeof(DTOMapping));



//  Authentication


var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIyIiwiZXhwIjoxNzc2MDk1MjA1fQ.EdAF-5gi31Fi_22W8wJA4Im0Jb2bLJZLQgz2pdKoxq0";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

builder.Services.AddAuthorization();



//  For Serilog

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        shared: true)
    .CreateLogger();

builder.Host.UseSerilog();




// Caching
builder.Services.AddMemoryCache();



//Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", opt =>
    {
        opt.PermitLimit = 5; // max 5 requests 
        opt.Window = TimeSpan.FromSeconds(5); // per 10 sec
        opt.QueueLimit = 2;
    });
});


// For Cron

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("CoinMarketJob");

    q.AddJob<CronMarketJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("CoinMarketJob-trigger")
        .WithCronSchedule("0 */10 * * * ?") // every 10 mins
    );
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();
