using Domain.Entity;
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
    public class DeleteCustomerRequest:IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeletecustomerHandler : IRequestHandler<DeleteCustomerRequest, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeletecustomerHandler(ICustomerRepository customerRepository,IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository; 
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                 _customerRepository.Delete(request.Id);
                _unitOfWork.saveChange();
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
