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
    public class CreateCustomer: IRequest<CreateNewCustomerResponse>
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

    public class CreateCustomerQueryHandler : IRequestHandler<CreateCustomer,CreateNewCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateCustomerQueryHandler(ICustomerRepository customerRepository,IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateNewCustomerResponse> Handle(CreateCustomer request, CancellationToken cancellationToken)
        {

            var customer = Customer.CreateNew(request.Id, request.FirstName, request.LastName,
                request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber);

            if (_customerRepository.GetByFirstNameAsync(request.FirstName).Result)
                return null;
            //throw new DuplicatesCustomerFirstNameException("FirstName is Esisting");
            if (_customerRepository.GetByLastNameAsync(request.LastName).Result)
                return null;
            //throw new DuplicatesCustomerLastNameException("lastName is Existing");

            if (_customerRepository.GetByDateOfBirtrhAsync(request.DateOfBirth).Result)
                return null;
            //throw new DuplicatesCustomerDateOfBirthException("DateOfBirth is existing");

            if (_customerRepository.GetByEmailAsync(request.Email).Result)
                return null;
            // throw new DuplicatesCustomerEmailException("Email Is Exixting");

            await _customerRepository.Create(customer);
              var customerId =   await _unitOfWork.SaveChangesAsync();
            var createNewCustomerResponse = new CreateNewCustomerResponse()
            {
                Id = customerId,
            };
            return createNewCustomerResponse;

        }
      



    }
}
