using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Offers.Builders;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OrangeBricks.Web.Tests.Controllers.Offers.Builders
{
    [TestFixture]
    public class OffersOnPropertyViewModelBuilderTest
    {
         private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();

            var properties = new List<Models.Property> {
                new Models.Property {
                    Id = 1, SellerUserId = "888", Description = "Small house", StreetName = "1 Somewhere", NumberOfBedrooms = 1, IsListedForSale = true, PropertyType = "House",
                    Offers = new List<Models.Offer> {
                        new Offer {  Id = 1, PropertyId = 1, Amount = 1000000, BuyerUserId = "123", Status = OfferStatus.Accepted },
                        new Offer {  Id = 2, PropertyId = 1, Amount = 2000000, BuyerUserId = "124", Status = OfferStatus.Pending }
                    }
                }         
            };            

            var mockPropertySet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockPropertySet);
        }

        [Test]
        public void BuildShouldReturnOffersForProperty()
        {
            // Arrange
            var builder = new OffersOnPropertyViewModelBuilder(_context);

            // Act
            var viewModel = builder.Build(1);

            // Assert
            Assert.That(viewModel.Offers.Count, Is.EqualTo(2));            
        }        
    }
}
