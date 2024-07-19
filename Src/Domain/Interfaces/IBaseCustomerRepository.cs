using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseCustomerRepository
    {
        List<Customer> GetAll();
        void Create(Customer customer);

        Customer GetById(long Id);
        Customer GetBy(string firstName);
        Customer GetByLastName(string lastName);
        Customer GetByPhoneNumber(string PhoneNumber);
        Customer GetByEmail(string Email);
        Customer GetByDateOfBirth(DateTime DateOfBirth);
        void Delete(long Id);

        Task<long> CreateAsync(Customer customer);
        Task<List<Customer>> GetAllAsync();

    }
}
