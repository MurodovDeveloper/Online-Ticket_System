using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Ticket.Application.UseCases.Common.Report
{
    public record GetUsersExcelByTelegram(string userId, string fileName) : IRequest;
    public class GetUsersExcelByTelegramHandler : IRequestHandler<GetUsersExcelByTelegram>
    {

        private readonly ITelegramBotClient _botClient;
        private readonly IMediator _mediator;

        public GetUsersExcelByTelegramHandler(ITelegramBotClient botClient, IMediator mediator)
        {
            _botClient = botClient;
            _mediator = mediator;
        }

        public async Task Handle(GetUsersExcelByTelegram request, CancellationToken cancellationToken)
        {
            var file = await _mediator.Send(new GetUsersExcel() { FileName = request.fileName });
            using (var ms = new MemoryStream(file.FileContents))
            {
                var OnlineFile = new InputOnlineFile(ms, file.FileName);

                await _botClient.SendDocumentAsync(request.userId, OnlineFile);

            }


        }
    }

    internal class InputOnlineFile : InputFile
    {
        private MemoryStream ms;
        private object fileName;

        public InputOnlineFile(MemoryStream ms, object fileName)
        {
            this.ms = ms;
            this.fileName = fileName;
        }
    }
}
