using Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query
{
    public class CheckDuplicatedCustomer : IRequest<CheckDuplicatedCustomerResponse>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
    }

    public class CheckDuplicatedCustomerResponse
    {

        public bool FirstName { get; set; }
        public bool LastName { get; set; }
        public bool DateOfBirth { get; set; }
        public bool Email { get; set; }
    }

    public class CheckDuplicatedCustomerHandler : IRequestHandler<CheckDuplicatedCustomer, CheckDuplicatedCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public CheckDuplicatedCustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CheckDuplicatedCustomerResponse> Handle(CheckDuplicatedCustomer request, CancellationToken cancellationToken)
        {
            var checkDuplicatedCustomerResponse = new CheckDuplicatedCustomerResponse();
            var listCustomers = await _customerRepository.GetAll();


            checkDuplicatedCustomerResponse.FirstName = listCustomers.Any(x => x.FirstName == request.FirstName) ?
                 true : false;

            checkDuplicatedCustomerResponse.LastName = listCustomers.Any(x => x.LastName == request.LastName) ?
               true : false;

            checkDuplicatedCustomerResponse.DateOfBirth = listCustomers.Any(x => x.DateOfBirth == request.DateOfBirth) ?
                true : false;

            checkDuplicatedCustomerResponse.Email = listCustomers.Any(x => x.Email == request.Email) ?
                true : false;

            return checkDuplicatedCustomerResponse;

        }
    }
}
