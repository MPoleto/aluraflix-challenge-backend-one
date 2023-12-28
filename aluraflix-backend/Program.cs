using Microsoft.EntityFrameworkCore;

using aluraflix_backend.Data;
using aluraflix_backend.Data.DAOs;
using aluraflix_backend.Data.DAOs.IDAOs;
using aluraflix_backend.Services;
using aluraflix_backend.Services.IServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var dbConnection = builder.Configuration.GetConnectionString("VideoConnection");

builder.Services.AddDbContext<AluraflixContext>(options => options
    .UseSqlServer(dbConnection));

//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IVideoDAO, VideoDAO>();
builder.Services.AddScoped<ICategoriaDAO, CategoriaDAO>();

builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();

builder.Services.AddControllers().AddNewtonsoftJson();
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

// {
//     "titulo": "Duna: Part Two",
//     "descricao": "Trailer oficial do filme Duna parte 2",
//     "url": "https://www.youtube.com/watch?v=U2Qp5pL3ovA&pp=ygUHdHJhaWxlcg%3D%3D",
//     "categoriaID": 9
// }

// [
//     {
//         "op": "replace",
//         "path": "/descricao",
//         "value": "Carole and Tuesday - trilha sonora"
//     }
// ]

/*
{
    "categoriaID": 13,
    "titulo": "Músicas para estudar",
    "cor": "#ff544d",
    "videos": [
        {
            "id": 10,
            "titulo": "Músicas J-POP",
            "descricao": "Playlist músicas aleatórias de j-pop",
            "url": "https://www.youtube.com/watch?v=6aQ4lK1xCcI",
            "categoriaID": 13
        }
    ]
}

{
        "categoriaID": 12,
        "titulo": "Animações",
        "cor": "#92cde8",
        "videos": []
    }
*/