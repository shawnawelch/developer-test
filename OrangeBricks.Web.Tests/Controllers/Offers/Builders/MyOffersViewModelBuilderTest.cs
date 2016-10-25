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
    public class MyOffersViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();

            var offers = new List<Models.Offer> {
                new Models.Offer {
                    Id = 1, PropertyId = 1, Amount = 100000, BuyerUserId = "123", Status = OfferStatus.Accepted,
                    Property = new Models.Property { Id = 1, Description = "Small house", StreetName = "1 Somewhere", NumberOfBedrooms = 1, IsListedForSale = true, PropertyType = "House" }
                },
                new Models.Offer {
                    Id = 2, PropertyId = 1,  Amount = 200000, BuyerUserId = "124", Status = OfferStatus.Pending,
                    Property = new Models.Property { Id = 1, Description = "Small house", StreetName = "1 Somewhere", NumberOfBedrooms = 1, IsListedForSale = true, PropertyType = "House" }
                },
                new Models.Offer {
                    Id = 3, PropertyId = 2, Amount = 300000, BuyerUserId = "123", Status = OfferStatus.Accepted,
                    Property = new Models.Property { Id = 2, Description = "Large house", StreetName = "2 Somewhere", NumberOfBedrooms = 8, IsListedForSale = true, PropertyType = "House" }
                },
                new Models.Offer {
                    Id = 4, PropertyId = 2, Amount = 400000, BuyerUserId = "125", Status = OfferStatus.Pending,
                    Property = new Models.Property { Id = 2, Description = "Large house", StreetName = "2 Somewhere", NumberOfBedrooms = 8, IsListedForSale = true, PropertyType = "House" }
                }
            };

            var mockOfferSet = Substitute.For<IDbSet<Models.Offer>>()
                .Initialize(offers.AsQueryable());

            _context.Offers.Returns(mockOfferSet);
        }

        [Test]
        public void BuildShouldReturnSingleOfferForABuyer()
        {
            // Arrange
            var builder = new MyOffersViewModelBuilder(_context);

            // Act
            var viewModel = builder.Build("124");

            // Assert
            Assert.That(viewModel.Count, Is.EqualTo(1));            
        }

        [Test]
        public void BuildShouldReturnMultipleOffersForABuyer()
        {
            // Arrange
            var builder = new MyOffersViewModelBuilder(_context);

            // Act
            var viewModel = builder.Build("123");

            // Assert
            Assert.That(viewModel.Count, Is.EqualTo(2));            
        }

        [Test]
        public void BuildShouldReturnNoOffersForABuyer()
        {
            // Arrange
            var builder = new MyOffersViewModelBuilder(_context);

            // Act
            var viewModel = builder.Build("999");

            // Assert
            Assert.That(viewModel.Count, Is.EqualTo(0));            
        }

    }
}
