using System.ComponentModel.DataAnnotations;

namespace EventEaseApp.Models
{
    public class Venue
    {
        [Key]
        public int VenueID { get; set; }

        [Required]
        [StringLength(250)]
        public string VenueName { get; set; }

        [Required]
        [StringLength(250)]
        public string Locations { get; set; }

        public int Capacity { get; set; }

        public string ImageUrl { get; set; }
    }
}
