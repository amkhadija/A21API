using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using A21API.Data;
using Pomelo.EntityFrameworkCore.MySql;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<A21APIContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("A21APIContext") ?? throw new InvalidOperationException("Connection string 'A21APIContext' not found.")));

// Add services to the container.
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

// Migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<A21APIContext>();
    context.Database.Migrate();
}

app.Run();
