using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class RequestViewingViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public RequestViewingViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public RequestViewingViewModel Build(int id)
        {
            var property = _context.Properties.Find(id);

            return new RequestViewingViewModel
            {
                PropertyId = property.Id,
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                ViewingAt = DateTime.Today.AddDays(1).AddHours(9)
            };
        }
    }
}