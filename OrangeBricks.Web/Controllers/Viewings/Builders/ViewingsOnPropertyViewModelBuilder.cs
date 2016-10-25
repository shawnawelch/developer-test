using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewings.Builders
{
    public class ViewingsOnPropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsOnPropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ViewingsOnPropertyViewModel Build(int id)
        {
            var property = _context.Properties
                .Where(p => p.Id == id)
                .Include(x => x.Viewings)
                .SingleOrDefault();

            var viewings = property.Viewings ?? new List<Viewing>();

            return new ViewingsOnPropertyViewModel
            {
                HasViewings = viewings.Any(),
                Viewings = viewings.Select(o => new ViewingViewModel
                {
                    Id = o.Id,
                    ViewingAt = o.ViewingAt,
                    CreatedAt = o.CreatedAt,
                    IsPending = o.Status == ViewingStatus.Pending,
                    Status = o.Status.ToString()
                }),
                PropertyId = property.Id, 
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                NumberOfBedrooms = property.NumberOfBedrooms
            };
        }
    }
}