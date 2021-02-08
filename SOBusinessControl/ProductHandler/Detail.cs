using MediatR;
using SODtaAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SODtaModel;
using SOBusinessControl.Domain;
using SODtaModel.View;

namespace SOBusinessControl.ProductHandler
{
    public class Detail
    {
        public class Query : IRequest<ProductDetailView>
        {
            public int Id { get; set; }
        }
        public class Handler : IRequestHandler<Query, ProductDetailView>
        {
            private readonly ProductBiz _productBiz;
            public Handler(IUnitOfWork unitofWork)
            {
                _productBiz = new ProductBiz(unitofWork);
            }

            public async Task<ProductDetailView> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _productBiz.GetDetailAsync(request.Id);
                return result;
            }
        }
    }
}
