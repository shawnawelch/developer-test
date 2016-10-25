using System.Data.Entity;

namespace OrangeBricks.Web.Models
{
    public interface IOrangeBricksContext
    {
        IDbSet<Property> Properties { get; set; }
        IDbSet<Offer> Offers { get; set; }
        IDbSet<Viewing> Viewings { get; set; }

        void SaveChanges();
    }
}