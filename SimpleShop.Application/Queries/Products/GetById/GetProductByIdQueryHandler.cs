using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleShop.Application.Modify.Exceptions;
using SimpleShop.Application.Modify.Extensions;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Queries.Products.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private ILogger Logger { get; }

        private IReadShopContext Context { get; }

        public GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger, IReadShopContext context)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await Context.Products.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (product == null) throw new NotFoundException($"Product with id: {request.Id} not found.");

            return product.ToProductDto();
        }
    }
}
