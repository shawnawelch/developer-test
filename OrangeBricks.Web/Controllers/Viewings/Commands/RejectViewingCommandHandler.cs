using System;
using OrangeBricks.Web.Models;
using System.Linq;

namespace OrangeBricks.Web.Controllers.Viewings.Commands
{
    public class RejectViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public RejectViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(RejectViewingCommand command)
        {
            var viewing = _context.Viewings.FirstOrDefault(o => o.Id == command.ViewingId);

            viewing.UpdatedAt = DateTime.Now;
            viewing.Status = ViewingStatus.Rejected;

            _context.SaveChanges();
        }
    }
}