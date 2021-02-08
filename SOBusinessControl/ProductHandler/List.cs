using MediatR;
using SOBusinessControl.Domain;
using SODtaAccess.Data.Repository.IRepository;
using SODtaModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOBusinessControl.ProductHandler
{
    public class List
    {
        public class Query : IRequest<List<Product>> { }
        public class Handler : IRequestHandler<Query, List<Product>>
        {
            private readonly ProductBiz _productBiz;
            public Handler(IUnitOfWork unitofWork)
            {
                _productBiz = new ProductBiz(unitofWork);
            }
            /*
                Calcellation Token is  
            */
            public async Task<List<Product>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _productBiz.GetAllAsync();
                return result;

            }
        }
    }
}
