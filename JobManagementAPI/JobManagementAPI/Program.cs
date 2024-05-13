using JobManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.OpenApi.Models;
//using System;
//using System.Text;
//using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    string authHeader = context.Request.Headers["Authorization"];

    if (authHeader != null && authHeader.StartsWith("Basic "))
    {
        string encCredentials = authHeader.Substring("Basic ".Length).Trim();
        string decCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encCredentials));
        string[] credentials = decCredentials.Split(':', 2);

        if (credentials.Length == 2 && AuthenticateUser(credentials[0], credentials[1]))
        {
            await next();
            return;
        }
    }

    context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Your Realm\"";
    context.Response.StatusCode = 401;
    await context.Response.WriteAsync("Unauthorized");
});
app.UseAuthorization();

app.MapControllers();

app.Run();
bool AuthenticateUser(string username, string password)
{
    return username == "User" && password == "Test@123";
}