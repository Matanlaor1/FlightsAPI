using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightsAPI.Data;
using FlightsAPI.Models;
using FlightsAPI.Services;

namespace FlightsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightsService _flightsService;

        public FlightsController(FlightsService service)
        {
            _flightsService = service;
        }

        // GET: api/Flights
        [HttpGet("Flights")]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            return await _flightsService.GetFlightsAsync();
        }

        [HttpGet("Logs")]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
        {
            return await _flightsService.GetLogsAsync();
        }
        
        [HttpPost]
        public void PostFlight(Flight flight)
        {
            _flightsService.AddFlight(flight);
        }


    }
}
