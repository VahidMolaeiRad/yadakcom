using Application.CQRS.Query;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interface;
using Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command
{
    public class CreateCustomer : IRequest<CreateNewCustomerResponse>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }

    public class CreateNewCustomerResponse
    {
        public long Id { get; set; }
    }

    public class CreateCustomerQueryHandler : IRequestHandler<CreateCustomer, CreateNewCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;
        public CreateCustomerQueryHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            this.mediator = mediator;
        }
        public async Task<CreateNewCustomerResponse> Handle(CreateCustomer request, CancellationToken cancellationToken)
        {

            var customer = Customer.CreateNew(request.Id, request.FirstName, request.LastName,
                request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber);


            var checkDuplicatedCustomerResponse = new CheckDuplicatedCustomer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email
            };


            var duplicated = await mediator.Send(checkDuplicatedCustomerResponse);


            if (duplicated.FirstName)
                throw new DuplicatesCustomerFirstNameException("FirstName is Esisting");

            if (duplicated.LastName)
                throw new DuplicatesCustomerLastNameException("lastName is Existing");

            if (duplicated.DateOfBirth)
                throw new DuplicatesCustomerDateOfBirthException("DateOfBirth is existing");

            if (duplicated.Email)
                throw new DuplicatesCustomerEmailException("Email Is Exixting");

            await _customerRepository.Create(customer);
            var customerId = await _unitOfWork.SaveChangesAsync();
            var createNewCustomerResponse = new CreateNewCustomerResponse()
            {
                Id = customerId,
            };
            return createNewCustomerResponse;

        }




    }
}
