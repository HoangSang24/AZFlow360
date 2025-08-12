using AutoMapper;
using AZFlow360.Domain.Entities;
using AZFlow360.Domain.Interfaces;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Application.Features.Auth.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public int CategoryID { get; set; }
        public int? SupplierID { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(255);

            RuleFor(v => v.CategoryID)
                .NotEmpty().WithMessage("Category is required.");
        }
    }
}
