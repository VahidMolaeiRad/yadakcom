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
    public class EditCustomer:IRequest<EditCustomerResponse>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }
    }
    public class EditCustomerResponse
    {
        public long Id { get; set; }
    }

    public class EditCustomerHandler : IRequestHandler<EditCustomer, EditCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator mediator;

        public EditCustomerHandler(ICustomerRepository customerRepository,IUnitOfWork unitOfWork, IMediator mediator)
        {
                _customerRepository = customerRepository;
                _unitOfWork = unitOfWork;
            this.mediator = mediator;
        }
        public async Task<EditCustomerResponse> Handle(EditCustomer request, CancellationToken cancellationToken)
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


            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync();

            var result = new EditCustomerResponse()
            {
                Id = customer.Id
            };
            return result;


        }

    }
}
