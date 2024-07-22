using Microsoft.AspNetCore.Mvc;
using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<ActionResult<List<CustomerRequestModel>>> Get(string? search, string? sortItem, string? sortOrder);
        Task<Customer> Post(CustomerRequestModel customer);
        Task<Customer> Update(Guid id, CustomerRequestModel requestModel);
        Task<Customer> Delete(Guid id);
    }
}
