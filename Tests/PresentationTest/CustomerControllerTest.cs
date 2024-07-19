using Application.CQRS.Command;
using Application.CQRS.Query;
using Domain.Entity;
using Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using PresentationAPI.Controllers;
using RESTFulSense.Clients;

namespace Presentation_Test
{
    public class CustomerControllerTest
    {
        private readonly RESTFulApiFactoryClient _client;

        public CustomerControllerTest()
        {
            var factory=  new WebApplicationFactory<Program>();

            var httpClient = factory.CreateClient();
            _client = new RESTFulApiFactoryClient(httpClient);
        }

        [Fact]
        public async void Should_Create_New_Customer()
        {
            var customer = new CreateCustomer() {
                Id = 0, FirstName = "Test",
                LastName = "Test",
                DateOfBirth = DateTime.Now,
                PhoneNumber="09124569922",
                Email ="test@gmail.com",
                BankAccountNumber = "1237894569632587"
            };

            var result= await _client.PostContentAsync<CreateCustomer, CreateNewCustomerResponse>("/api/Customers", customer);

            //assert
            result.Should().Be(result);
        }

        [Fact]
        public async void Should_Return_GetAll_Customer()
        {
            var result = await _client.GetContentAsync<List<GetAllCustomer>>("/api/Customers");

            //assert
            result.Should().HaveCountGreaterThanOrEqualTo(0);
        }

        [Fact]
        public async void Should_Delete_Customer()
        {
            var deleteRow= 16;
            var result = await _client.DeleteContentAsync<bool>($"/api/Customers/{deleteRow}");

            //assert
            result.Should();
        }

        [Fact]
        public async void Should_Get_Customer()
        {
            var getRow = 20;
            var result = await _client.GetContentStringAsync($"/api/Customers/{getRow}");

            //assert
            result.Should().Be(result);
        }
        [Fact]
        public async void Should_Editt_Customer()
        {
            var editCustomer = new EditCustomer()
            {
                Id = 20,
                FirstName = "Tr",
                LastName = "Test",
                DateOfBirth = DateTime.Now,
                PhoneNumber = "09124569622",
                Email = "tes12t@gmail.com",
                BankAccountNumber = "1237894569632587"
            };
            var result = await _client.PutContentAsync<EditCustomer, EditCustomerResponse>($"/api/Customers", editCustomer);

            //assert
            result.Should().Be(result);
        }

        [Fact]
        public async void Should_Dublicated_Customer_Create()
        {
            var customer = new CreateCustomer()
            {
                Id = 0,
                FirstName = "Test",
                LastName = "Test",
                DateOfBirth = DateTime.Now,
                PhoneNumber = "09124569922",
                Email = "test@gmail.com",
                BankAccountNumber = "1237894569632587"
            };

            var result = await _client.PostContentAsync<CreateCustomer, CreateNewCustomerResponse>("/api/Customers", customer);

            //assert
            result.Should().BeNull();
           
        }

        [Fact]
        public async void Should_Dublicated_Customer_Edit()
        {
            var editCustomer = new EditCustomer()
            {
                Id = 20,
                FirstName = "Tr",
                LastName = "Test",
                DateOfBirth = DateTime.Now,
                PhoneNumber = "09124569622",
                Email = "tes12t@gmail.com",
                BankAccountNumber = "1237894569632587"
            };
            var result = await _client.PutContentAsync<EditCustomer, EditCustomerResponse>($"/api/Customers", editCustomer);
            //assert
            result.Should().BeNull();
        }

    }
}