using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Domain.Validators;
using Domain.Entities;
using Infrastructure.Data;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Infrastructure.Glue;
using employeeSystem.api.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EmployeeValidator>());

builder.Services.AddDependencies(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Employee API",
        Version = "v1",
        Description = "API for managing employees",
        Contact = new OpenApiContact
        {
            Name = "Nelson Ruddy Paulino de Jesus",
            Email = "nelsonruddy@gmail.com"
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API v1");
    });
}

app.UseRouting();

app.UseMiddleware<ExceptionMiddleware>();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
