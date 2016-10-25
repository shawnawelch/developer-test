using System.Data.Entity;
using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using OrangeBricks.Web.Controllers.Offers.Commands;

namespace OrangeBricks.Web.Tests.Controllers.Offers.Commands
{
    [TestFixture]
    public class RejectOfferCommandHandlerTest
    {
        private RejectOfferCommandHandler _handler;
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();
                        
            var offers = new List<Models.Offer> {
                new Offer {  Id = 1, PropertyId = 1, Amount = 1000000, BuyerUserId = "123", Status = OfferStatus.Pending },
            };

            var mockOfferSet = Substitute.For<IDbSet<Models.Offer>>()
                .Initialize(offers.AsQueryable());

            _context.Offers.Returns(mockOfferSet);
            
            _handler = new RejectOfferCommandHandler(_context);
        }

        [Test]
        public void HandleShouldAcceptOffer()
        {
            // Arrange
            var command = new RejectOfferCommand
            {
                PropertyId = 1,
                OfferId = 1
            };

            // Act
            _handler.Handle(command);

            // Assert            
            var offer = _context.Offers.First();

            Assert.That(offer.Status, Is.EqualTo(OfferStatus.Rejected));            
        }        
    }
}
