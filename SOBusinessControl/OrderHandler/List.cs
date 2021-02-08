using MediatR;
using SOBusinessControl.Domain;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOBusinessControl.OrderHandler
{
    
    public class List
    {
        public class Query : IRequest<List<Order>> { }
        public class Handler : IRequestHandler<Query, List<Order>>
        {
            private readonly OrderBiz _orderBiz;
            public Handler(IUnitOfWork unitofWork)
            {
                _orderBiz = new OrderBiz(unitofWork);
            }
          
            public async Task<List<Order>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _orderBiz.GetAllAsync();
                return result;

            }
        }
    }
}
