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
    public class MyViewingsViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();

            var viewings = new List<Models.Viewing> {
                new Models.Viewing {
                    Id = 1, PropertyId = 1, ViewingAt = new DateTime(2017,1,1,9,0,0), BuyerUserId = "123", Status = ViewingStatus.Accepted,
                    Property = new Models.Property { Id = 1, Description = "Small house", StreetName = "1 Somewhere", NumberOfBedrooms = 1, IsListedForSale = true, PropertyType = "House" }
                },
                new Models.Viewing {
                    Id = 2, PropertyId = 1, ViewingAt = new DateTime(2017,1,1,10,0,0), BuyerUserId = "124", Status = ViewingStatus.Pending,
                    Property = new Models.Property { Id = 1, Description = "Small house", StreetName = "1 Somewhere", NumberOfBedrooms = 1, IsListedForSale = true, PropertyType = "House" }
                },
                new Models.Viewing {
                    Id = 3, PropertyId = 2, ViewingAt = new DateTime(2017,1,1,11,0,0), BuyerUserId = "123", Status = ViewingStatus.Accepted,
                    Property = new Models.Property { Id = 2, Description = "Large house", StreetName = "2 Somewhere", NumberOfBedrooms = 8, IsListedForSale = true, PropertyType = "House" }
                },
                new Models.Viewing {
                    Id = 4, PropertyId = 2, ViewingAt = new DateTime(2017,1,1,12,0,0), BuyerUserId = "125", Status = ViewingStatus.Pending,
                    Property = new Models.Property { Id = 2, Description = "Large house", StreetName = "2 Somewhere", NumberOfBedrooms = 8, IsListedForSale = true, PropertyType = "House" }
                }
            };

            var mockViewingSet = Substitute.For<IDbSet<Models.Viewing>>()
                .Initialize(viewings.AsQueryable());

            _context.Viewings.Returns(mockViewingSet);
        }

        [Test]
        public void BuildShouldReturnSingleViewingForABuyer()
        {
            // Arrange
            var builder = new MyViewingsViewModelBuilder(_context);

            // Act
            var viewModel = builder.Build("124");

            // Assert
            Assert.That(viewModel.Count, Is.EqualTo(1));            
        }

        [Test]
        public void BuildShouldReturnMultipleViewingsForABuyer()
        {
            // Arrange
            var builder = new MyViewingsViewModelBuilder(_context);

            // Act
            var viewModel = builder.Build("123");

            // Assert
            Assert.That(viewModel.Count, Is.EqualTo(2));            
        }

        [Test]
        public void BuildShouldReturnNoViewingsForABuyer()
        {
            // Arrange
            var builder = new MyViewingsViewModelBuilder(_context);

            // Act
            var viewModel = builder.Build("999");

            // Assert
            Assert.That(viewModel.Count, Is.EqualTo(0));            
        }

    }
}
