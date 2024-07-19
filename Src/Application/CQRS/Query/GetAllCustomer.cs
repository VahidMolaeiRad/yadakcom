using Domain.Entity;
using Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query
{
    
    public class GetAllCustomer :IRequest<List<GetAllCustomer>>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomer, List<GetAllCustomer>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetAllCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository; 
        }
        public async Task<List<GetAllCustomer>> Handle(GetAllCustomer request, CancellationToken cancellationToken)
        {
            var listCustomers =await _customerRepository.GetAll();
            var customers = new List<GetAllCustomer>();
            customers = listCustomers.Select(s => new GetAllCustomer()).ToList();
            return customers;
        }
    }
}
