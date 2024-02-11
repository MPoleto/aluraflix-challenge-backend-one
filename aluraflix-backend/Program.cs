using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using aluraflix_backend.Data;
using aluraflix_backend.Data.DAOs;
using aluraflix_backend.Data.DAOs.IDAOs;
using aluraflix_backend.Services;
using aluraflix_backend.Services.IServices;
using aluraflix_backend.Models;


var builder = WebApplication.CreateBuilder(args);

var videoConnection = builder.Configuration.GetConnectionString("VideoConnection");

var userConnection = builder.Configuration.GetConnectionString("UserConnection");

builder.Services.AddDbContext<AluraflixContext>(options => options
    .UseSqlServer(videoConnection));
    
builder.Services.AddDbContext<UsuarioContext>(options => options
    .UseSqlServer(userConnection));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IVideoDAO, VideoDAO>();
builder.Services.AddScoped<ICategoriaDAO, CategoriaDAO>();

builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = 
            new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])
            ),
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


/*
{
    "Username": "daniel",
    "DataNascimento": "1900-01-01",
    "Password": "Senha123!",
    "RePassword": "Senha123!"
},
{
    "Username": "romulo",
    "Email": "romulo@email.com",
    "Password": "Senha123@",
    "RePassword": "Senha123@"
},
{
    "Username": "pedro",
    "Email": "pedro@email.com",
    "Password": "Teste789!",
    "RePassword": "Teste789!"
},
{
    "Username": "Maria",
    "Email": "maria@email.com",
    "Password": "Teste000*",
    "RePassword": "Teste000*"
}
*/