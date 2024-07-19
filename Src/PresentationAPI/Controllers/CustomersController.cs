using Application.CQRS.Command;
using Application.CQRS.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllCustomer());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var  customer=new GetCustomer() { Id = id };
            var result = await mediator.Send(customer);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomer createCustomer)
        {
            var result = await mediator.Send(createCustomer);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) 
        {
            var model = new DeleteCustomerRequest
            {
                Id = id
            };
            
            var result = await mediator.Send(model);
            return Ok(result);
        }

        [HttpPut()]
        public async Task<IActionResult> Put(EditCustomer model)
        {
            

            var result = await mediator.Send(model);
            return Ok(result);
        }
    }
}
