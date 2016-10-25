using System;
using OrangeBricks.Web.Models;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class AcceptOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public AcceptOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(AcceptOfferCommand command)
        {
            var offer = _context.Offers.FirstOrDefault(o => o.Id == command.OfferId);

            offer.UpdatedAt = DateTime.Now;
            offer.Status = OfferStatus.Accepted;

            _context.SaveChanges();
        }
    }
}