using AutoMapper;
using MarketManager.Application.Common.Interfaces;
using MarketManager.Domain.Entities;
using MediatR;

namespace MarketManager.Application.UseCases.Items.Queries.GetAllItems
{
    public record GetAllItemsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<List<ItemResponse>>;

    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, List<ItemResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAllItemsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<List<ItemResponse>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Item> items = _context.Items;

            return Task.FromResult(_mapper.Map<List<ItemResponse>>(items));

        }
    }
    public class ItemResponse
    {
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public Guid OrderId { get; set; }
        public double Count { get; set; }
        public double SoldPrice { get; set; }
    }
}
