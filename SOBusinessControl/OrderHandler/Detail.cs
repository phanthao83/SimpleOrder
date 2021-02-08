using MediatR;
using SOBusinessControl.Domain;
using SOBusinessControl.Ultility;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using SODtaModel.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOBusinessControl.OrderHandler
{
    public class Detail
    {
        public class Query : IRequest<OrderDetailView>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, OrderDetailView>
        {
            private readonly OrderBiz _orderBiz;
            public Handler(IUnitOfWork unitofWork)
            {
                _orderBiz = new OrderBiz(unitofWork);
            }

            public async Task<OrderDetailView> Handle(Query request, CancellationToken cancellationToken)
            {
                /*Noted that OrderDetailOption is optinal. */
                var result = await _orderBiz.GetOrderDetailAsync(request.Id);
                return result;
            }

        }
    }
}
