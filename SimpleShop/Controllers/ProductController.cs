using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.Application.Modify.Commands.Products.Create;
using SimpleShop.Application.Modify.Commands.Products.Update;
using SimpleShop.Application.Modify.Models;
using SimpleShop.Application.Modify.Queries.Products.GetAll;
using SimpleShop.Application.Modify.Queries.Products.GetById;

namespace SimpleShop.ModifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator Mediator { get; }

        public ProductController(IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpGet]
        public async Task<List<ProductDto>> Get()
        {
            var query = new GetProductsQuery();

            var response = await Mediator.Send(query);

            return response;
        }
        
        [HttpGet("{id}")]
        public async Task<ProductDto> Get(long id)
        {
            var query = new GetProductByIdQuery(id);

            return await Mediator.Send(query);
        }
        
        [HttpPost]
        public async Task<ProductDto> Post([FromBody] CreateProductCommand command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpPut("{id}")]
        public async Task<ProductDto> Put(long id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            
            return await Mediator.Send(command);
        }
    }
}
