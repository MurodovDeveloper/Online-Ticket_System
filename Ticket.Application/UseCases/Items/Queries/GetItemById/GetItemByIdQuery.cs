using AutoMapper;
using MarketManager.Application.Common.Interfaces;
using MarketManager.Domain.Entities;
using MediatR;

namespace MarketManager.Application.UseCases.Items.Queries.GetItemById
{
    public record GetItemByIdQuery(Guid Id) : IRequest<GetItemByIdQueryResponse>;

    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, GetItemByIdQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetItemByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetItemByIdQueryResponse> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            Item? item = await _context.Items.FindAsync(request.Id);

            if (item is null)
                throw new NotFoundException(nameof(Item), request.Id);

            return _mapper.Map<GetItemByIdQueryResponse>(item);
        }
    }
    public class GetItemByIdQueryResponse
    {
        public Guid Id { get; set; }

        public double Count { get; set; }
        public double SoldPrice { get; set; }

        public Guid PackageId { get; set; }
        public Guid OrderId { get; set; }
    }
}
