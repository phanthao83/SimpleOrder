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

namespace SOBusinessControl.OrderHandler
{
    public class Create
    {
        public class Command : IRequest
        {
            public OrderDetailView OrderDetail { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly OrderBiz _orderBiz;
            public Handler(IUnitOfWork unitofWork)
            {
                _orderBiz = new OrderBiz(unitofWork);
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                var success = await _orderBiz.CreateOrderAsync(request.OrderDetail);
                if (success) return Unit.Value;
                else
                    throw new RestException(HttpStatusCode.InternalServerError, new { order = "Unable to save order" });
            }
           
        }
    }
}
