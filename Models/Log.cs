namespace FlightsAPI.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int LegId { get; set; }
        public DateTime In {  get; set; }
        public DateTime? Out { get; set; }
        public Status Status { get; set; }

    }
}
