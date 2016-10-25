using System;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class RequestViewingCommand
    {
        public int PropertyId { get; set; }

        public DateTime ViewingAt { get; set; }

        public string BuyerUserId { get; set; }
    }
}