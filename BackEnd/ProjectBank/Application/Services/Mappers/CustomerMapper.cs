using ProjectBank.Entities;
using ProjectBank.Models;

namespace ProjectBank.Application.Services.Mappers
{
    public class CustomerMapper
    {
        public Customer MapRequestToCustomer(CustomerRequestModel requestModel)
        {
            var customer = new Customer();
            customer.Id = Guid.NewGuid();
            customer.Name = requestModel.Name;
            customer.LastName = requestModel.LastName;
            customer.Country = requestModel.Country;
            customer.Phone = requestModel.Phone;
            customer.Email = requestModel.Email;

            return customer;
        }


        public CustomerRequestModel MapRequestToDB(Customer customer)
        {
            var requestModel = new CustomerRequestModel();
            requestModel.Name = customer.Name;
            requestModel.LastName = customer.LastName;
            requestModel.Country = customer.Country;
            requestModel.Phone = customer.Phone;
            requestModel.Email = customer.Email;

            return requestModel;
        }

        public Customer MapRequestToSet(Customer res, CustomerRequestModel customer)
        {
            res.Name = customer.Name;
            res.LastName = customer.LastName;
            res.Country = customer.Country;
            res.Phone = customer.Phone;
            res.Email = customer.Email;

            return res;
        }
    }
}
