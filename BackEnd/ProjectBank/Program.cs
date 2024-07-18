﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Controller.Controllers.Mappers;
using ProjectBank.Controller.Services;
using ProjectBank.Controller.Validators;
using ProjectBank.Data;
using ProjectBank.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<Account>());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICardService, CardServise>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IMoneyTransferService, MoneyTransferService>();
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddScoped<AbstractValidator<Account>, AccountValidator>();
builder.Services.AddScoped<IValidator<Account>, AccountValidator>();

builder.Services.AddScoped<AccountMapper>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});


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

app.UseCors();

app.Run();