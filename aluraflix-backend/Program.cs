using aluraflix_backend.Data;
using aluraflix_backend.Data.DTOs;
using aluraflix_backend.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var dbConnection = builder.Configuration.GetConnectionString("VideoConnection");

builder.Services.AddDbContext<AluraflixContext>(options => options
    .UseSqlServer(dbConnection));

//builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<VideoDAO>();
builder.Services.AddScoped<CategoriaDAO>();

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
//     "titulo": "Message in the wind",
//     "descricao": "Música do anime Carole and Tuesday",
//     "url": "https://www.youtube.com/watch?v=KDrX-gOiCV4&list=PLPw4vQ0H5dTTwhUDJ5gDMqCa5aDGESei7&index=17&pp=iAQB8AUB"
// }

// [
//     {
//         "op": "replace",
//         "path": "/descricao",
//         "value": "Carole and Tuesday - trilha sonora"
//     }
// ]