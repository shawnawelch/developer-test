using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OrangeBricks.Web.Controllers.Viewings.Builders
{
    public class MyViewingsViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyViewingsViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public List<MyViewingViewModel> Build(string userId)
        {
            var viewings = _context.Viewings
                .Where(o => o.BuyerUserId == userId)
                .Include(o => o.Property)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new MyViewingViewModel
                {
                    ViewingId = o.Id,
                    PropertyId = o.PropertyId,
                    NumberOfBedrooms = o.Property.NumberOfBedrooms,                                        
                    PropertyType = o.Property.PropertyType,
                    StreetName = o.Property.StreetName,
                    ViewingAt = o.ViewingAt,
                    Status = o.Status.ToString(),
                    CreatedAt = o.CreatedAt                    
                }).ToList();

            return viewings;
        }
    }
}