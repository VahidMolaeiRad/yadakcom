using Domain.Interfaces;
using Domain.Repository;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.CQRS.Command;
using System.Reflection;
using Application.CQRS.Query;
using Domain.Interface;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IBaseCustomerRepository, BaseCustomerRepository>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<CustomerDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CleanArchitectureConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCustomer)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateCustomerMockRequest)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetListcustomerRequest)));

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
