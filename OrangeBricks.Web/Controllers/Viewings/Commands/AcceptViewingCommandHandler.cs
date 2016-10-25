using System;
using OrangeBricks.Web.Models;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Viewings.Commands
{
    public class AcceptViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public AcceptViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(AcceptViewingCommand command)
        {
            var viewing = _context.Viewings.FirstOrDefault(o => o.Id == command.ViewingId);

            viewing.UpdatedAt = DateTime.Now;
            viewing.Status = ViewingStatus.Accepted;

            _context.SaveChanges();
        }
    }
}