using System;
using System.Collections.Generic;
using OrangeBricks.Web.Models;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class RequestViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public RequestViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(RequestViewingCommand command)
        {
            var property = _context.Properties.FirstOrDefault(o => o.Id == command.PropertyId);

            var viewing = new Viewing
            {
                ViewingAt = command.ViewingAt,
                Status = ViewingStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                BuyerUserId = command.BuyerUserId
            };

            if (property.Viewings == null)
            {
                property.Viewings = new List<Viewing>();
            }
                
            property.Viewings.Add(viewing);
            
            _context.SaveChanges();
        }
    }
}