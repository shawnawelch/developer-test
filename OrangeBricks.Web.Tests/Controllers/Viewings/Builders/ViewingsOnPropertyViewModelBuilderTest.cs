using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Viewings.Builders;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OrangeBricks.Web.Tests.Controllers.Viewings.Builders
{
    [TestFixture]
    public class ViewingsOnPropertyViewModelBuilderTest
    {
         private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();

            var properties = new List<Models.Property> {
                new Models.Property {
                    Id = 1, SellerUserId = "888", Description = "Small house", StreetName = "1 Somewhere", NumberOfBedrooms = 1, IsListedForSale = true, PropertyType = "House",
                    Viewings = new List<Models.Viewing> {
                        new Viewing {  Id = 1, PropertyId = 1, ViewingAt = new DateTime(2017,1,1,9,0,0), BuyerUserId = "123", Status = ViewingStatus.Accepted },
                        new Viewing {  Id = 2, PropertyId = 1, ViewingAt = new DateTime(2017,1,1,10,0,0), BuyerUserId = "124", Status = ViewingStatus.Pending }
                    }
                }         
            };            

            var mockPropertySet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockPropertySet);
        }

        [Test]
        public void BuildShouldReturnViewingsForProperty()
        {
            // Arrange
            var builder = new ViewingsOnPropertyViewModelBuilder(_context);

            // Act
            var viewModel = builder.Build(1);

            // Assert
            Assert.That(viewModel.Viewings.Count, Is.EqualTo(2));            
        }        
    }
}
