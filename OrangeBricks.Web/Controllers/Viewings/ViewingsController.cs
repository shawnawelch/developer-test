using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Viewings.Builders;
using OrangeBricks.Web.Controllers.Viewings.Commands;
using OrangeBricks.Web.Models;
using System.Web.Mvc;

namespace OrangeBricks.Web.Controllers.Viewings
{
    public class ViewingsController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsController(IOrangeBricksContext context)
        {
            _context = context;
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult OnProperty(int id)
        {
            var builder = new ViewingsOnPropertyViewModelBuilder(_context);
            var viewModel = builder.Build(id);

            return View(viewModel);
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        [HttpPost]        
        public ActionResult Accept(AcceptViewingCommand command)
        {
            var handler = new AcceptViewingCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        [HttpPost]
        public ActionResult Reject(RejectViewingCommand command)
        {
            var handler = new RejectViewingCommandHandler(_context);

            handler.Handle(command);

            return RedirectToAction("OnProperty", new { id = command.PropertyId });
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MyViewings()
        {
            var builder = new MyViewingsViewModelBuilder(_context);
            var viewModel = builder.Build(User.Identity.GetUserId());

            return View(viewModel);
        }
    }
}