using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Mappers
{
    public interface ICustomerMapper
    {
        List<CustomerRequestModel> GetRequestModels(List<Customer> customers);
        Customer GetCustomer(CustomerRequestModel customerRequest);
        Customer PutRequestModelInCustomer(Customer customer, CustomerRequestModel customerRequest);
    }
}
