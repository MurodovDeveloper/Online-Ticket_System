using AutoMapper;
using MarketManager.Application.Common.Interfaces;
using MarketManager.Application.Common.Models;
using MarketManager.Domain.Entities;
using MediatR;
namespace MarketManager.Application.UseCases.Clients.Queries.GetAllClients;

public record GetAllClientsQuery : IRequest<PaginatedList<GetAllClientsQueryResponse>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, PaginatedList<GetAllClientsQueryResponse>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllClientsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<GetAllClientsQueryResponse>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Clients
            .Select(p => _mapper.Map<Client, GetAllClientsQueryResponse>(p));
        return await PaginatedList<GetAllClientsQueryResponse>.CreateAsync(query, request.PageNumber, request.PageSize);
    }
}
public class GetAllClientsQueryResponse
{
    public Guid Id { get; set; }
    public double TotalPrice { get; set; }
    public double Discount { get; set; }
}
