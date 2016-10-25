using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Offers.ViewModels
{
    public class MyViewingViewModel
    {
        public int PropertyId { get; set; }
        public int ViewingId { get; set; }

        public string PropertyType { get; set; }
        public int NumberOfBedrooms{ get; set; }
        public string StreetName { get; set; }        
                                
        public DateTime ViewingAt { get; set; }
        public DateTime CreatedAt { get; set; }        
        public string Status { get; set; }
    }
}