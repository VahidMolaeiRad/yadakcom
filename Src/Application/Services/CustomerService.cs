using Domain.Entity;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        void Edit(Customer editCustomer);
        long Create(Customer createCustomer);
        void Delete(long id);

    }




    public class CustomerService : ICustomerService
    {
        private readonly IBaseCustomerRepository _customerRepository;
        public CustomerService(IBaseCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
 

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }


        public long Create(Customer createCustomer)
        {
            if (_customerRepository.GetBy(createCustomer.FirstName) != null)

                throw new DuplicatesCustomerFirstNameException();

            if (_customerRepository.GetByLastName(createCustomer.LastName) != null)

                throw new DuplicatesCustomerFirstNameException();

            if (_customerRepository.GetByPhoneNumber(createCustomer.PhoneNumber) != null)

                throw new DuplicatesCustomerPhoneNumberException();

            if (_customerRepository.GetByEmail(createCustomer.Email) != null)

                throw new DuplicatesCustomerEmailException();

            if (_customerRepository.GetByDateOfBirth(createCustomer.DateOfBirth) != null)

                throw new DuplicatesCustomerEmailException();


            var customer = new Customer(createCustomer.Id, createCustomer.FirstName, createCustomer.LastName,
                createCustomer.DateOfBirth, createCustomer.PhoneNumber, createCustomer.Email,  createCustomer.BankAccountNumber);
            _customerRepository.Create(customer);
            return customer.Id;


        }


        public void Edit(Customer editCustomer)
        {
            if (_customerRepository.GetBy(editCustomer.FirstName) != null)

                throw new DuplicatesCustomerFirstNameException();

            if (_customerRepository.GetByLastName(editCustomer.LastName) != null)

                throw new DuplicatesCustomerFirstNameException();

            if (_customerRepository.GetByPhoneNumber(editCustomer.PhoneNumber) != null)

                throw new DuplicatesCustomerPhoneNumberException();

            if (_customerRepository.GetByEmail(editCustomer.Email) != null)

                throw new DuplicatesCustomerEmailException("");
            if (_customerRepository.GetByDateOfBirth(editCustomer.DateOfBirth) != null)

                throw new DuplicatesCustomerEmailException();

            _customerRepository.Delete(editCustomer.Id);
            var customer = new Customer(editCustomer.Id, editCustomer.FirstName, editCustomer.LastName,
             editCustomer.DateOfBirth,  editCustomer.PhoneNumber ,editCustomer.Email, editCustomer.BankAccountNumber);
            _customerRepository.Create(customer);


        }
        public void Delete(long id)
        {
            _customerRepository.Delete(id);
        }

    }
}
