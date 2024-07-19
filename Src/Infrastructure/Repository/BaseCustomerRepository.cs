using Domain.Entity;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BaseCustomerRepository : IBaseCustomerRepository
    {
        public List<Customer> Customers = new List<Customer>()
        {
            new Customer(1,"vahid","Molaeirad",DateTime.Now,"09026394420","molaeirad1992@gmail.com","1234567896532148")
        };
        public List<Customer> GetAll()
        {
            return Customers;
        }
        public void Create(Customer customer)
        {
            Customers.Add(customer);
        }

        public Customer GetById(long Id)
        {
            return Customers.FirstOrDefault(x => x.Id == Id);
        }

        public Customer GetBy(string firstName)
        {
            return Customers.FirstOrDefault(x => x.FirstName == firstName);
        }

        public Customer GetByLastName(string lastName)
        {
            return Customers.FirstOrDefault(x => x.LastName == lastName);
        }

        public Customer GetByPhoneNumber(string PhoneNumber)
        {
            return Customers.FirstOrDefault(x => x.PhoneNumber == PhoneNumber);
        }

        public Customer GetByEmail(string Email)
        {
            return Customers.FirstOrDefault(x => x.Email == Email);
        }
        public Customer GetByDateOfBirth(DateTime DateOfBirth)
        {
            return Customers.FirstOrDefault(x => x.DateOfBirth == DateOfBirth);
        }
     

        public void Delete(long Id)
        {
            var customer = GetById(Id);
            Customers.Remove(customer);
        }


        public async Task<long> CreateAsync(Customer customer)
        {
            Customers.Add(customer);
            return customer.Id;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return Customers;
        }
    }
}
