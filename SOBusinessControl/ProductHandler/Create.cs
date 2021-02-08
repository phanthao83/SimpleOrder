using FluentValidation;
using MediatR;
using SOBusinessControl.Domain;
using SOBusinessControl.Ultility;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using SODtaModel.View;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOBusinessControl.ProductHandler
{
    public class Create
    {
        public class Command : IRequest
        {
            public ProductDetailView ProductInfo { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ProductInfo.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.ProductInfo.Description).MaximumLength(200);
                RuleForEach(x => x.ProductInfo.ProductOptions).SetValidator(new ProductOptionValidator());
            }
        }

        public class ProductOptionValidator : AbstractValidator<ProductOptionView>
        {
            public ProductOptionValidator()
            {
                RuleFor(x => x.OptionDescription).MaximumLength(200); 
            }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly ProductBiz _productBiz;
            public Handler(IUnitOfWork unitofWork)
            {
                _productBiz = new ProductBiz(unitofWork);
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                
                var success = await _productBiz.CreateProductAsync(request.ProductInfo);
                return Unit.Value;
            }
        }
    }
}
