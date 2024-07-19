using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Customer GetById(long id);
        Task<Customer> GetByIdAsync(long id);
        Task<long> Create(Customer customer);
        long Update(Customer customer);
        void Delete(long id);

        Task<bool> GetByFirstNameAsync(string firstName);
        Task<bool> GetByDateOfBirtrhAsync (DateTime date);
        Task<bool> GetByLastNameAsync(string lastName);
        Task<bool> GetByEmailAsync(string email);
    }

}
