using AutoMapper;
using MarketManager.Application.Common.Interfaces;
using MarketManager.Domain.Entities;
using MediatR;

namespace MarketManager.Application.UseCases.Items.Commands.UpdateItem
{
    public class UpdateItemCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid PackageId { get; set; }
        public Guid OrderId { get; set; }
        public double Count { get; set; }
        public double SoldPrice { get; set; }
    }
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateItemCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            Item? item = await _context.Items.FindAsync(request.Id);
            _mapper.Map(item, request);
            if (item is null)
                throw new NotFoundException(nameof(Item), request.Id);
            var package = await _context.Packages.FindAsync(request.PackageId);
            if (package is null)
                throw new NotFoundException(nameof(Package), request.PackageId);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
