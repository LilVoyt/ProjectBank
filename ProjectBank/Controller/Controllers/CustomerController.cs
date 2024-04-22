using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBank.Data;
using ProjectBank.Entities;

namespace ProjectBank.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        //Customer
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(Guid id)
        {
            var account = await _context.Customers.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(await GetAllCustomers()); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var account = await _context.Customers.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class AccountController : ControllerBase
        {
            private readonly DataContext _context;

            public AccountController(DataContext context)
            {
                _context = context;
            }
            //Account
            [HttpGet]
            public async Task<ActionResult<List<Account>>> GetAllAccounts()
            {
                var customers = await _context.Accounts.ToListAsync();

                return Ok(customers);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Account>> GetAccount(Guid id)
            {
                var account = await _context.Accounts.FindAsync(id);

                if (account == null)
                {
                    return NotFound();
                }

                return account;
            }

            [HttpPost]
            public async Task<ActionResult<List<Account>>> AddAccount(Account account)
            {
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                return Ok(await GetAllAccounts()); //Maybe error
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateAccount(Guid id, Account account)
            {
                if (id != account.Id)
                {
                    return BadRequest();
                }

                _context.Entry(account).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAccount(Guid id)
            {
                var account = await _context.Accounts.FindAsync(id);
                if (account == null)
                {
                    return NotFound();
                }

                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
}
