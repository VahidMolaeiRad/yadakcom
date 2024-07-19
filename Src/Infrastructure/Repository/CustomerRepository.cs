using Domain.Entity;
using Domain.Repository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }
     

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public Customer GetById(long id)
        {
            var data=  _context.Customers.First(x=> x.Id == id);
            return data;
        }
        public async Task<Customer> GetByIdAsync(long id)
        {
            var data =await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }
        public async Task<long> Create(Customer customer)
        {
             await _context.Customers.AddAsync(customer);
            return customer.Id;
        }

     
        public long Update(Customer customer)
        {
            _context.Update(customer);
            return customer.Id;
        }

        public  void Delete(long id)
        {
            var customer = GetById(id);
             _context.Customers.Remove(customer);
        }

        public async Task<bool> GetByFirstNameAsync(string firstName)
        {
            var hasCustomer =await _context.Customers.AnyAsync(x => x.FirstName == firstName);
            return hasCustomer;
        }

        public async Task<bool> GetByLastNameAsync(string lastName)
        {
            var hasCustomer = await _context.Customers.AnyAsync(x => x.LastName == lastName);
            return hasCustomer;
        }

        public async Task<bool> GetByDateOfBirtrhAsync(DateTime dateTime)
        {
            var hasCustomer = await _context.Customers.AnyAsync(x => x.DateOfBirth == dateTime);
            return hasCustomer;
        }
        public async Task<bool> GetByEmailAsync(string email)
        {
            var hasCustomer = await _context.Customers.AnyAsync(x => x.Email == email);
            return hasCustomer;
        }

    }
}
