using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Commands
{
    [TestFixture]
    public class RejectViewingCommandHandlerTest
    {
        private RejectViewingCommandHandler _handler;
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
                        
            var viewings = new List<Models.Viewing> {
                new Viewing {  Id = 1, PropertyId = 1, ViewingAt = new DateTime(2017,1,1,9,0,0), BuyerUserId = "123", Status = ViewingStatus.Pending },
            };

            var mockViewingSet = Substitute.For<IDbSet<Models.Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Viewings.Returns(mockViewingSet);
            
            _handler = new RejectViewingCommandHandler(_context);
        }

        [Test]
        public void HandleShouldAcceptViewing()
        {
            // Arrange
            var command = new RejectViewingCommand
            {
                PropertyId = 1,
                ViewingId = 1
            };

            // Act
            _handler.Handle(command);

            // Assert            
            var viewing = _context.Viewings.First();

            Assert.That(viewing.Status, Is.EqualTo(ViewingStatus.Rejected));            
        }        
    }
}
