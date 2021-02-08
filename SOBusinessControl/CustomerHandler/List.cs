using MediatR;
using SOBusinessControl.Domain;
using SODtaAccess.Data.Repository.IRepository;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SOBusinessControl.CustomerHandler
{
    public class List
    {

        public class Query : IRequest<List<SODtaModel.Customer>> {      }
        public class Handler : IRequestHandler<Query, List<SODtaModel.Customer>>
        {
            private readonly CustomerBiz _customerBiz;
            public Handler(IUnitOfWork unitofWork)
            {
                _customerBiz = new CustomerBiz(unitofWork);
            }

            /*
                Calcellation Token is  
            */
            public async Task<List<SODtaModel.Customer>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _customerBiz.GetAllAsync(); 
                return result; 

            }
        }

    }
}
