using AutoMapper;
using ClosedXML.Excel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Drawing;
using Ticket.Application.Common.Interfaces;

namespace Ticket.Application.UseCases.Common.Report
{
    public class GetUsersExcel : IRequest<ExcelReportResponse>
    {
        public string FileName { get; set; }
    }
    public class GetUsersExcelHandler : IRequestHandler<GetUsersExcel, ExcelReportResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersExcelHandler(IApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<ExcelReportResponse> Handle(GetUsersExcel request, CancellationToken cancellationToken)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var userData = await GetUsersAsync();
                var sheet1 = wb.AddWorksheet(userData, "Users");


                sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

                sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

                sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;

                sheet1.Row(1).Style.Font.FontColor = XLColor.White;

                sheet1.Row(1).Style.Font.Bold = true;
                sheet1.Row(1).Style.Font.Shadow = true;
                sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
                sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
                sheet1.Row(1).Style.Font.Italic = true;

                sheet1.RowHeight = 20;
                sheet1.Column(1).Width = 38;
                sheet1.Column(2).Width = 20;
                sheet1.Column(3).Width = 20;
                sheet1.Column(4).Width = 20;



                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    return new ExcelReportResponse(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{request.FileName}.xlsx");

                }
            }
        }

        private async Task<DataTable> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            var allUser = await _context.Users.ToListAsync(cancellationToken);

            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("Code", typeof(Guid));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Phone", typeof(string));


            var _list = _mapper.Map<List<UserResponse>>(allUser);
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.Id, item.FullName, item.Username, item.Phone);

                });
            }

            return dt;
        }

    }
}
