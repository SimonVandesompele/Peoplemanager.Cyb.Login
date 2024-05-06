using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PeopleManager.Core;
using PeopleManager.RestApi.Services;
using PeopleManager.RestApi.Settings;
using PeopleManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PeopleManagerDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(PeopleManagerDbContext));
    //options.UseSqlServer("Server=.\\SqlExpress;Database=PeopleManager;Trusted_Connection=True;TrustServerCertificate=true");
});

var jwtSettings = new JwtSettings();
builder.Configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

// within this section we are configuring the authentication and setting the default scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(jwt =>
    {
        if (!string.IsNullOrWhiteSpace(jwtSettings.Secret))
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, // this will validate the 3rd part of the jwt token using the secret that we added in the appsettings and verify we have generated the jwt token
                IssuerSigningKey = new SymmetricSecurityKey(key), // Add the secret key to our Jwt encryption
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        }
    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<PeopleManagerDbContext>();

builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<AssignmentService>();
builder.Services.AddScoped<PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<PeopleManagerDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
