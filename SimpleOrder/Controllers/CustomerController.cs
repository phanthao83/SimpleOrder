using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SODtaAccess.Data.Repository.IRepository;
using System.Text.Json;
using SODtaModel;
using System.Threading.Tasks;
using MediatR;
using SOBusinessControl.CustomerHandler;
using SODtaModel.View;

namespace SimpleOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //private readonly IUnitOfWork _unitOfWork; 
        private readonly IMediator _mediator;
        
        public CustomerController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
         public async Task<ActionResult<List<Customer>>> List()
        {
            return await _mediator.Send(new List.Query());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailView>> Detail(int id)
        {
         
            return await _mediator.Send(new Detail.Query { Id = id });

        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command )
        {
            return await _mediator.Send(command);
        }


    }
}
