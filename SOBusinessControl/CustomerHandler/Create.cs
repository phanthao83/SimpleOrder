using MediatR;
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
using FluentValidation;
using SOBusinessControl.Domain;

namespace SOBusinessControl.CustomerHandler
{
    public class Create
    {
        public class Command : IRequest
        {
               public CustomerDetailView CustomerDetail { get; set; }

        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.CustomerDetail.Name).NotEmpty().MaximumLength(100);
                RuleFor(x => x.CustomerDetail.Email).MaximumLength(100).EmailAddress();
                RuleForEach(x=>x.CustomerDetail.AddressLst).SetValidator(new AddressValidator());
            }
        }

        public class AddressValidator : AbstractValidator<CustomerAddressView>
        {
            public AddressValidator()
            {
                RuleFor(x => x.Address1).NotEmpty().MaximumLength(50);
                RuleFor(x => x.Address2).MaximumLength(50);
                RuleFor(x => x.ShopName).NotEmpty().MaximumLength(100);
                RuleFor(x => x.City).NotEmpty().MaximumLength(50);
                RuleFor(x => x.State).NotEmpty().MaximumLength(2);
                RuleFor(x => x.ZipCode).NotEmpty().MaximumLength(5);
                RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(10);
                RuleFor(x => x.AddressType).NotEmpty();

            }
        }

        public class Handler : IRequestHandler<Command >
        {
            private readonly CustomerBiz _customerBiz; 
            public Handler(IUnitOfWork unitofWork)
            {
                _customerBiz = new CustomerBiz(unitofWork);
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var success = await _customerBiz.CreateCustomerAsync(request.CustomerDetail); 
                if (success) return Unit.Value;
                else
                    throw new Exception("Problem in saving");
            }

            
        }
    }
}
