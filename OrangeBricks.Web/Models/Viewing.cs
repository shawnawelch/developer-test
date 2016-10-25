using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrangeBricks.Web.Models
{
    public class Viewing
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        public DateTime ViewingAt { get; set; }
        public ViewingStatus Status { get; set; }
                
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        public string BuyerUserId { get; set; }        
        
        public virtual Property Property { get; set; }
    }
}