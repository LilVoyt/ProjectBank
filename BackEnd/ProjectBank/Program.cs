using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Application.Controllers.Exceptions;
using ProjectBank.Application.Services.FunctionalityService;
using ProjectBank.Application.Services.Interfaces;
using ProjectBank.Application.Services.Mappers;
using ProjectBank.Application.Validators.Account;
using ProjectBank.Application.Validators.Card;
using ProjectBank.Application.Validators.Customer;
using ProjectBank.Application.Validators.Employee;
using ProjectBank.Application.Validators.Transaction;
using ProjectBank.Controller.Services;
using ProjectBank.Data;
using ProjectBank.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

builder.Services.AddControllers()
    .AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<Account>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataContext, DataContext>();


builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IMoneyTransferService, MoneyTransferService>();

builder.Services.AddScoped<CreditCardGenerator>();

builder.Services.AddScoped<IAccountValidationService, AccountValidationService>();
builder.Services.AddScoped<ICustomerValidationService, CustomerValidationService>();
builder.Services.AddScoped<IEmployeeValidationService, EmployeeValidationService>();
builder.Services.AddScoped<ICardValidationService, CardValidationService>();
builder.Services.AddScoped<ITransactionValidationService, TransactionValidationService>();

builder.Services.AddScoped<AbstractValidator<Account>, AccountValidator>();
builder.Services.AddScoped<IValidator<Account>, AccountValidator>();

builder.Services.AddScoped<AbstractValidator<Customer>, CustomerValidator>();
builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();

builder.Services.AddScoped<AbstractValidator<Employee>, EmployeeValidator>();
builder.Services.AddScoped<IValidator<Employee>, EmployeeValidator>();

builder.Services.AddScoped<AbstractValidator<Card>, CardValidator>();
builder.Services.AddScoped<IValidator<Card>, CardValidator>();

builder.Services.AddScoped<AbstractValidator<Transaction>, TransactionValidator>();
builder.Services.AddScoped<IValidator<Transaction>, TransactionValidator>();

builder.Services.AddScoped<AccountMapper>();
builder.Services.AddScoped<ICustomerMapper, CustomerMapper>();
builder.Services.AddScoped<EmployeeMapper>();
builder.Services.AddScoped<CardMapper>();
builder.Services.AddScoped<TransactionMapper>();



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

var app = builder.Build();

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
