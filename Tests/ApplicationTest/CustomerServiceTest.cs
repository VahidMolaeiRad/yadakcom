using Application.Services;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Test.Builders;
using FluentAssertions;
using NSubstitute;

namespace Application_Test
{
    public class CustomerServiceTest
    {

        private readonly CustomerService _customerService;
        private readonly IBaseCustomerRepository _customerRepository;
        private readonly CustomerTestBuilder customerBuilder;
        public CustomerServiceTest()
        {
            _customerRepository = NSubstitute.Substitute.For<IBaseCustomerRepository>();
            _customerService = new CustomerService(_customerRepository);
            customerBuilder = new CustomerTestBuilder();
        }

        [Fact]
        public void Should_GetAll_List_Customer()
        {
            //arrang
            _customerRepository.GetAll().Returns(new List<Customer>());

            // act
            var customer = _customerService.GetAll();

            //assert
            customer.Should().BeOfType<List<Customer>>();
            _customerRepository.Received().GetAll();

        }

        [Fact]
        public void Shoul_Create_Add_New_Customer_ReturnID()
        {
            // arrange
            var customer = customerBuilder.Build();

            // act
            var actual = _customerService.Create(customer);
            // assert
            actual.Should().Be(customer.Id);
        }
         [Fact]
        public void Should_Throw_Exception_When_Add_Duplicated_Customer()
        {
            // arrange
            var customerExist = customerBuilder.Build();

           
            _customerRepository.GetBy(Arg.Any<string>()).Returns(customerExist);



            Action actual=()=> _customerService.Create(customerExist);

            actual.Should().Throw<DuplicatesCustomerFirstNameException>();

        }

        [Fact]
        public void Shoild_Update_Customer()
        {

            // arrange
            var customerExist = customerBuilder.Build();

            //act
            _customerService.Edit(customerExist);

            // assert
            Received.InOrder(() =>
            {
                _customerRepository.Delete(customerExist.Id);
                _customerRepository.Create(Arg.Any<Customer>());

            });
        }
        [Fact]
        public void Should_Delete_Customer()
        {
            // arrange
            const long id = 10;

            //act
            _customerService.Delete(id);

            //assert
            _customerRepository.Received().Delete(id);
        }

    }
}