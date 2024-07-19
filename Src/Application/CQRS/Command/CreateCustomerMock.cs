using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command
{
   
    public class CreateCustomerMockRequest : IRequest<CreateCustomerResponse>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class CreateCustomerResponse
    {
        public long Id { get; set; }
    }

    public class CustomerQueryHandler : IRequestHandler<CreateCustomerMockRequest, CreateCustomerResponse>
    {
        private readonly IBaseCustomerRepository _customerRepository;

        public CustomerQueryHandler(IBaseCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CreateCustomerResponse> Handle(CreateCustomerMockRequest request, CancellationToken cancellationToken)
        {
            var customer = Customer.CreateNew(request.Id, request.FirstName, request.LastName, request.DateOfBirth,
                request.PhoneNumber, request.Email, request.BankAccountNumber);
            var customerId = await _customerRepository.CreateAsync(customer);
            var saveCustomerResponse = new CreateCustomerResponse
            {
                Id = customerId
            };
            return saveCustomerResponse;
        }



    }
}
