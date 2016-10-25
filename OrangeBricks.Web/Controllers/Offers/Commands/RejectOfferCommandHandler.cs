using System;
using OrangeBricks.Web.Models;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class RejectOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public RejectOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(RejectOfferCommand command)
        {
            var offer = _context.Offers.FirstOrDefault(o => o.Id == command.OfferId);

            offer.UpdatedAt = DateTime.Now;
            offer.Status = OfferStatus.Rejected;

            _context.SaveChanges();
        }
    }
}