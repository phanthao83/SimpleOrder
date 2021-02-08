using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using SODtaModel;
using System.Threading.Tasks;
using System.Threading;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel.View;
using System.Net;
using SOBusinessControl.Ultility;
using SOBusinessControl.Domain;

namespace SOBusinessControl.CustomerHandler
{
    public class Detail
    {
        public class Query : IRequest<CustomerDetailView> { 
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, CustomerDetailView>
        {
            private readonly CustomerBiz _customerBiz;

            public Handler(IUnitOfWork unitofWork)
            {
                _customerBiz = new CustomerBiz(unitofWork);
            }
            
            public async Task<CustomerDetailView> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _customerBiz.GetDetailAsync(request.Id); 
                return result;
            }
        }
    }
}
