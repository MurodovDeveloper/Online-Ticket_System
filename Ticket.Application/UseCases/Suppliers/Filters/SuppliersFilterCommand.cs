using MarketManager.Application.Common.Interfaces;
using MarketManager.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MarketManager.Application.UseCases.Suppliers.Filters;

public class SuppliersFilterCommand : IRequest<List<Supplier>>
{
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool OrderByName { get; set; } = false;
    public bool OrderByPhone { get; set; } = false;
}
public class SuppliersFilterCommandHandler : IRequestHandler<SuppliersFilterCommand, List<Supplier>>
{
    private readonly IApplicationDbContext _context;

    public SuppliersFilterCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Supplier>> Handle(SuppliersFilterCommand request, CancellationToken cancellationToken)
    {
        if (request.StartDate is null)
            request.StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-30));

        if (request.EndDate is null)
            request.EndDate = DateOnly.FromDateTime(DateTime.Now);

        if (request.OrderByName)
        {
            return await _context.Suppliers
                .Where(supplier => DateOnly.FromDateTime(supplier.CreatedDate) >= request.StartDate && DateOnly.FromDateTime(supplier.CreatedDate) <= request.EndDate)
                .OrderBy(x => x.Name).ToListAsync(cancellationToken);
        }
        else if (request.OrderByPhone)
        {
            return await _context.Suppliers
                .Where(supplier => DateOnly.FromDateTime(supplier.CreatedDate) >= request.StartDate && DateOnly.FromDateTime(supplier.CreatedDate) <= request.EndDate)
                .OrderBy(x => x.Phone).ToListAsync(cancellationToken);
        }
        else
        {
            return await _context.Suppliers
                .Where(supplier => DateOnly.FromDateTime(supplier.CreatedDate) >= request.StartDate && DateOnly.FromDateTime(supplier.CreatedDate) <= request.EndDate)
                .OrderBy(x => x.Name).ToListAsync(cancellationToken);
        }
    }
}