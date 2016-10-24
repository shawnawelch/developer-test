using NUnit.Framework;
using NSubstitute;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using System.Data.Entity;
using OrangeBricks.Web.Tests;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class MakeOfferCommandHandlerTest
    {
        private MakeOfferCommandHandler _handler;
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
                                
            // Setup offer mocks
            var mockOfferSet = Substitute.For<IDbSet<Models.Offer>>();
            _context.Offers.Returns(mockOfferSet);

            _handler = new MakeOfferCommandHandler(_context);
        }
        
        [Test]
        public void HandlerShouldAddOffer()
        {
            // Arrange           
            var command = new MakeOfferCommand
            {
                PropertyId = 1                
            };

            // Act
            _handler.Handle(command);

            // Assert
            var property = _context.Properties.First();
            Assert.IsNotEmpty(property.Offers);            
        }

        [Test]
        public void HandlerShouldAddOfferWithCorrectOfferValue()
        {
            // Arrange
            var command = new MakeOfferCommand
            {
                PropertyId = 1,
                Offer = 250000
            };

            // Act
            _handler.Handle(command);

            // Assert
            var property = _context.Properties.First();            
            Assert.IsNotEmpty(property.Offers);

            var offer = property.Offers.First();
            Assert.AreEqual(250000, offer.Amount);                            
        }

        [Test]
        public void HandlerShouldAddOfferWithCorrectBuyerUserId()
        {
            // Arrange
            var command = new MakeOfferCommand
            {
                PropertyId = 1,
                BuyerUserId = "123"
            };

            // Act
            _handler.Handle(command);

            // Assert
            var property = _context.Properties.First();            
            Assert.IsNotEmpty(property.Offers);

            var offer = property.Offers.First();
            Assert.AreEqual("123", offer.BuyerUserId);                            
        }

    }
}
