using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SimpleOrder.Controllers;
using SODtaAccess.Data.Repository.IRepository;

namespace NUnitTestSOProject.API
{
    public class OrderAPI
    {

        private OrderController _controller; 
        [SetUp]
        public void Setup()
        {
            //var mockUnit = new Mock<IUnitOfWork>();

            var mockMediator = new Mock<IMediator>();
             _controller = new OrderController(mockMediator.Object);

        }

        [Test]
        public void List_ReturnActionResultAsync()
        {

            var action =  _controller.List();
            
            // assert
            Assert.NotNull(action);
            Assert.NotNull(action.Result);
            Assert.IsTrue(action.Result.GetType() == typeof(ActionResult<List<SODtaModel.Order>>));
            
        }


        [Test]
        public void Detail_ReturnActionResult()
        {

            var action = _controller.Detail(1);
            Assert.NotNull(action);
            Assert.NotNull(action.Result);
            Assert.IsTrue(action.Result.GetType() == typeof(ActionResult<SODtaModel.View.OrderDetailView>));
        }

        [Test]
        public void Create_ReturnActionResult()
        {

             var mockCommand = new Mock<SOBusinessControl.OrderHandler.Create.Command>();
            var action = _controller.Create(mockCommand.Object);
            string msg = action.Result.ToString();
            Assert.NotNull(action);
            Assert.NotNull(action.Result);
            Assert.IsTrue(action.Result.GetType() == typeof(ActionResult<MediatR.Unit>));


        }
    }
}
