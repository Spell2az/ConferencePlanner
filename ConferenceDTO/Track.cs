using System.ComponentModel.DataAnnotations;

namespace ConferenceDTO
{
    public class Track
    {
        public int TrackId { get; set; }

        [Required]
        public int ConferenceId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }
    }
}