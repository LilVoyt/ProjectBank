﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Services;
using ProjectBank.Controller.Services.MathodsServise;
using ProjectBank.Controller.Validators;
using ProjectBank.Data;
using ProjectBank.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<Account>()); //and else
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICardService, CardServise>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IMethodsSevice, MethodsService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddScoped<AbstractValidator<Account>, AccountValidator>();
builder.Services.AddScoped<IValidator<Account>, AccountValidator>();


//Here changes
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

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
