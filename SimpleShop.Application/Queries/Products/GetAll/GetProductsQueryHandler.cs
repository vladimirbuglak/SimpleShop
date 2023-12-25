using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleShop.Application.Modify.Extensions;
using SimpleShop.Application.Modify.Interfaces;
using SimpleShop.Application.Modify.Models;

namespace SimpleShop.Application.Modify.Queries.Products.GetAll
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private ILogger Logger { get; }

        private IReadShopContext Context { get; }

        public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IReadShopContext context)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await Context.Products.ToListAsync(cancellationToken);

            return products.Select(x => x.ToProductDto()).ToList();
        }
    }
}
