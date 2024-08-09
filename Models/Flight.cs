namespace FlightsAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public Guid number { get; set; }
        public Status status { get; set; }
    }
}
