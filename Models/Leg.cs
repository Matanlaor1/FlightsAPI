using System.ComponentModel.DataAnnotations;

namespace FlightsAPI.Models
{
    public class Leg
    {
        public int Id { get; set; }
        public Flight? flight { get; set; }
        [Range(0,10000),Required]
        public int CrossingTime { get; set; }
        public int nextLegId { get; set; }
    }
}
