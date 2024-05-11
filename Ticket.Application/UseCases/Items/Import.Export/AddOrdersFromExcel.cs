using AutoMapper;
using ClosedXML.Excel;
using MarketManager.Application.Common.Interfaces;
using MarketManager.Application.UseCases.Items.Queries.GetAllItems;
using MediatR;
using Microsoft.AspNetCore.Http;
using Item = MarketManager.Domain.Entities.Item;

namespace MarketManager.Application.UseCases.Items.Import.Export;

public record AddItemsFromExcel(IFormFile ExcelFile) : IRequest<List<ItemResponse>>;

public class AddItemsFromExcelHandler : IRequestHandler<AddItemsFromExcel, List<ItemResponse>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddItemsFromExcelHandler(IApplicationDbContext context, IMapper mapper)
    {

        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ItemResponse>> Handle(AddItemsFromExcel request, CancellationToken cancellationToken)
    {
        if (request.ExcelFile == null || request.ExcelFile.Length == 0)
            throw new ArgumentNullException("File", "file is empty or null");

        var file = request.ExcelFile;
        List<Item> result = new();
        using (var ms = new MemoryStream())
        {

            await file.CopyToAsync(ms, cancellationToken);
            using (var wb = new XLWorkbook(ms))
            {
                var sheet1 = wb.Worksheet(1);
                int startRow = 2;
                for (int row = startRow; row <= sheet1.LastRowUsed().RowNumber(); row++)
                {
                    var item = new Item()
                    {
                        PackageId = Guid.Parse(sheet1.Cell(row, 1).GetString()),
                        OrderId = Guid.Parse(sheet1.Cell(row, 2).GetString()),
                        Count = double.Parse(sheet1.Cell(row, 3).GetString()),
                        SoldPrice = double.Parse(sheet1.Cell(row, 4).GetString()),

                    };


                    result.Add(item);
                }


            }
        }
        await _context.Items.AddRangeAsync(result);
        await _context.SaveChangesAsync();
        return _mapper.Map<List<ItemResponse>>(result);



    }
}

