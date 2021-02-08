using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOBusinessControl.OrderHandler;
using SODtaModel;
using SODtaModel.View;

namespace SimpleOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> List()
        {
            return await _mediator.Send(new List.Query());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailView>> Detail(int id)
        {

            return await _mediator.Send(new Detail.Query { Id = id });

        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}
