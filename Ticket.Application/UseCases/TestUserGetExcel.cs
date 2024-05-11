using MarketManager.Application.Common;
using MarketManager.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ticket.Application.Common.Interfaces;
using Ticket.Domain.Entities.Identity;

namespace MarketManager.Application.UseCases;
public class TestUserGetExcel : IRequest<ExcelReportResponse>
{
    public string FileName { get; set; }
}
public class TestGetExcelProductHandler : IRequestHandler<TestUserGetExcel, ExcelReportResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly GenericExcelReport _generic;

    public TestGetExcelProductHandler(IApplicationDbContext context, GenericExcelReport generic)
    {
        _context = context;
        _generic = generic;

    }

    public async Task<ExcelReportResponse> Handle(TestUserGetExcel request, CancellationToken cancellationToken)
    {

        var result = await _generic.GetReportExcel<User, UsersResponseExcelReport>(request.FileName, await _context.Users.ToListAsync(cancellationToken), cancellationToken);
        return result;
    }
}
