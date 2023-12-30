using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Modify.Commands.Orders;
using SimpleShop.Application.Modify.Commands.Orders.Cancel;
using SimpleShop.Application.Modify.Commands.Orders.Create;
using SimpleShop.Application.Modify.Models;
using SimpleShop.Application.Modify.Queries.Orders.GetAll;
using SimpleShop.Application.Modify.Queries.Orders.GetById;

namespace SimpleShop.ModifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMediator Mediator { get; }

        public OrderController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet]
        public async Task<List<OrderDto>> Get()
        {
            var query = new GetOrdersQuery();
        
            var response = await Mediator.Send(query);
        
            return response;
        }
        
        [HttpGet("{id}")]
        public async Task<OrderDto> Get(long id)
        {
            var query = new GetOrderByIdQuery(id);
        
            return await Mediator.Send(query);
        }
        
        [HttpPut("cancel/{id}", Name = "[controller]/Put")]
        public async Task Cancel(long id)
        {
            var command = new CancelOrderCommand(id);
        
            await Mediator.Send(command);
        }
        
        [HttpPost]
        public async Task<OrderDto> Post([FromBody] CreateOrderCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
