using NUnit.Framework;
using NSubstitute;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class RequestViewingCommandHandlerTest
    {
        private RequestViewingCommandHandler _handler;
        private IOrangeBricksContext _context;
                
        [SetUp]
        public void SetUp()
        {
            // Setup context mocks
            _context = Substitute.For<IOrangeBricksContext>();            

            // Setup propery mocks
            var properties = new List<Models.Property> {
                new Models.Property { Id = 1, Description = "Small house", StreetName = "1 Somewhere", NumberOfBedrooms = 1, IsListedForSale = true, PropertyType = "House" }                
            };

            var mockPropertySet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockPropertySet);
                                            
            _handler = new RequestViewingCommandHandler(_context);
        }
        
        [Test]
        public void HandlerShouldAddViewing()
        {
            // Arrange           
            var command = new RequestViewingCommand
            {
                PropertyId = 1                
            };

            // Act
            _handler.Handle(command);

            // Assert
            var property = _context.Properties.First();
            Assert.IsNotEmpty(property.Viewings);            
        }

        [Test]
        public void HandlerShouldAddViewingWithCorrectViewingAtValue()
        {
            // Arrange
            var command = new RequestViewingCommand
            {
                PropertyId = 1,
                ViewingAt = new DateTime(2017, 1, 1, 9, 0, 0)
            };

            // Act
            _handler.Handle(command);

            // Assert
            var property = _context.Properties.First();            
            Assert.IsNotEmpty(property.Viewings);

            var viewing = property.Viewings.First();
            Assert.AreEqual(new DateTime(2017, 1, 1, 9, 0, 0), viewing.ViewingAt);                            
        }

        [Test]
        public void HandlerShouldAddViewingWithCorrectBuyerUserId()
        {
            // Arrange
            var command = new RequestViewingCommand
            {
                PropertyId = 1,
                BuyerUserId = "123"
            };

            // Act
            _handler.Handle(command);

            // Assert
            var property = _context.Properties.First();            
            Assert.IsNotEmpty(property.Viewings);

            var viewing = property.Viewings.First();
            Assert.AreEqual("123", viewing.BuyerUserId);                            
        }
    }
}
