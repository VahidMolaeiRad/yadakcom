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
    public class GetCustomer:IRequest<Customer>
    {
        public long Id { get; set; }
    }

    public class GetCustomerHandler : IRequestHandler<GetCustomer, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerHandler(ICustomerRepository customerRepository)
        {
                _customerRepository = customerRepository;
        }
        public async Task<Customer> Handle(GetCustomer request, CancellationToken cancellationToken)
        {
            var customer =await _customerRepository.GetByIdAsync(request.Id);

            return customer;
        }
    }
}
