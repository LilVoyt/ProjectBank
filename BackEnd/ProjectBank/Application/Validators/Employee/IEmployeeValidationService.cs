﻿namespace ProjectBank.Application.Validators.Employee
{
    public interface IEmployeeValidationService
    {
        Task<bool> Is_PhoneNumber_Valid(string number, CancellationToken cancellationToken);
        Task<bool> Is_PhoneNumber_Not_In_DB(string number, CancellationToken cancellationToken);
        Task<bool> Is_Email_Not_In_DB(string email, CancellationToken cancellationToken);
    }
}
