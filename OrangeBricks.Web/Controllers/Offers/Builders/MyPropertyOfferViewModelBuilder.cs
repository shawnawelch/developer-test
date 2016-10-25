using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class MyOffersViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyOffersViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public List<MyOfferViewModel> Build(string userId)
        {
            var offers = _context.Offers
                .Where(o => o.BuyerUserId == userId)
                .Include(o => o.Property)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new MyOfferViewModel
                {
                    OfferId = o.Id,
                    PropertyId = o.PropertyId,
                    NumberOfBedrooms = o.Property.NumberOfBedrooms,                                        
                    PropertyType = o.Property.PropertyType,
                    StreetName = o.Property.StreetName,
                    Amount = o.Amount,
                    Status = o.Status.ToString(),
                    CreatedAt = o.CreatedAt                    
                }).ToList();

            return offers;
        }
    }
}