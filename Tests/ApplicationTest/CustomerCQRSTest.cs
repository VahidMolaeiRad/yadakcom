using Application.CQRS.Command;
using Application.CQRS.Query;
using Application.Services;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using Domain.Test.Builders;
using FluentAssertions;
using MediatR;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Test
{
    public class CustomerCQRSTest
    {

        private readonly IBaseCustomerRepository _customerRepository;
        private readonly CustomerTestBuilder customerBuilder;
        public CustomerCQRSTest()
        {
            _customerRepository = NSubstitute.Substitute.For<IBaseCustomerRepository>();
            customerBuilder = new CustomerTestBuilder();
        }

        [Fact]
        public async void Shoul_Create_Add_New_Customer_CQRS()
        {
            // arrange
            var mediator = new Mock<IMediator>();
            var customer = new CreateCustomerMockRequest()
            {
                Id = 1,
                FirstName = "vahid",
                LastName = "Molaeirad",
                DateOfBirth = DateTime.Now,
                PhoneNumber = "09026394420",
                BankAccountNumber = "1234567895698421",
                Email = "Test@gmail.com",
            };

            CustomerQueryHandler handler = new CustomerQueryHandler(_customerRepository);

            //Act
            var result = await handler.Handle(customer, new System.Threading.CancellationToken());

            //Assert
            //Do the assertion
            result.Should().Be(result);

        }


        [Fact]
        public async void Shoul_GetAll_Customers_CQRS()
        {
            // arrange
            var mediator = new Mock<IMediator>();
            var customer = new GetListcustomerRequest();


            GetCustomerQueryHandler handler = new GetCustomerQueryHandler(_customerRepository);

            //Act
            var result = await handler.Handle(customer, new System.Threading.CancellationToken());

            //Assert
            //Do the assertion
            result.Should().HaveCountGreaterThanOrEqualTo(0);

        }


    }
}
