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
        public EditCustomerHandler(ICustomerRepository customerRepository,IUnitOfWork unitOfWork)
        {
                _customerRepository = customerRepository;
                _unitOfWork = unitOfWork;
        }
        public async Task<EditCustomerResponse> Handle(EditCustomer request, CancellationToken cancellationToken)
        {
            var customer = Customer.CreateNew(request.Id, request.FirstName, request.LastName,
                request.DateOfBirth, request.PhoneNumber, request.Email, request.BankAccountNumber);

            if (_customerRepository.GetByFirstNameAsync(request.FirstName).Result)

                //throw new DuplicatesCustomerFirstNameException("FirstName is Esisting");
                return null;
            if (_customerRepository.GetByLastNameAsync(request.LastName).Result)

                return null ;


            if (_customerRepository.GetByDateOfBirtrhAsync(request.DateOfBirth).Result)

                return null;

            if (_customerRepository.GetByEmailAsync(request.Email).Result)

                //throw new DuplicatesCustomerEmailException("Email is exixting");
                return null;

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
