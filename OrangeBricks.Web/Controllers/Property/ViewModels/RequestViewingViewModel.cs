using System;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class RequestViewingViewModel
    {
        public string PropertyType { get; set; }
        public string StreetName { get; set; }
        public DateTime ViewingAt { get; set; }
        public int PropertyId { get; set; }
    }
}