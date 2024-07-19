using Application.CQRS.Command;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query
{
        public record GetListcustomerRequest():IRequest<List<GetListCustomerResponse>>;

    public class GetListCustomerResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }


    public class GetCustomerQueryHandler : IRequestHandler<GetListcustomerRequest, List<GetListCustomerResponse>>
    {
        private readonly IBaseCustomerRepository _customerRepository;

        public GetCustomerQueryHandler(IBaseCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<List<GetListCustomerResponse>> Handle(GetListcustomerRequest request, CancellationToken cancellationToken)
        {


              var customers = new List<Customer>()
                    {
                        new Customer(1,"vahid","Molaeirad",DateTime.Now,"09026394420","molaeirad1992@gmail.com","1234567896532148")
                    };
        var getListCustomerResponse = new List<GetListCustomerResponse>();
            getListCustomerResponse = customers.Select(s => new GetListCustomerResponse()
            { 
                Id=s.Id,
                FirstName=s.FirstName,
                LastName=s.LastName,
                Email=s.Email,
                DateOfBirth=s.DateOfBirth,
                PhoneNumber=s.PhoneNumber,
                BankAccountNumber=s.BankAccountNumber 
            }).ToList();
            return getListCustomerResponse;
        }



    }



}
