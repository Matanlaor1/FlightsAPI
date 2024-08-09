using ClientSide.Models;
using System.Net.Http.Json;

namespace ClientLogic
{
    public class FlightService
    {
        readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:7159") };
        public void AddFlight(FlightDto flight) => client.PostAsJsonAsync("api/Flights", flight);


        public Task<IEnumerable<LogDto>> GetLogs() => client.GetFromJsonAsync<IEnumerable<LogDto>>("api/Flights")!;
    }
}
