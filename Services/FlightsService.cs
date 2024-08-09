using FlightsAPI.Data;
using FlightsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightsAPI.Services
{
    public class FlightsService
    {
        private readonly DataContext _dataContext;
        private bool IsOperating = false;
        private List<Leg> legs;

        public FlightsService(DataContext dataContext)
        {
            _dataContext = dataContext;

            legs = _dataContext.Legs.ToList();

        }


        public async Task<List<Flight>> GetFlightsAsync()
        {
            return await _dataContext.Flights.ToListAsync();
        }

        public async Task<List<Log>> GetLogsAsync()
        {
            return await _dataContext.Logs.ToListAsync();
        }

        public void AddFlight(Flight flight)
        {
            int currentNumFlights = legs.Count(leg => leg.flight != null);
            if (currentNumFlights < 4)
            {
                _dataContext.Flights.Add(flight);
                _dataContext.SaveChanges();

                AddToFirstLeg(flight);
            }
            else { Console.WriteLine("Max flights reached"); }

        }

        public void AddToFirstLeg(Flight flight)
        {
            legs[0].flight = flight;
            AddLog(flight, legs[0]);
            IsOperating = true;
            Move();
        }

        public void Move()
        {
            while (IsOperating)
            {
                foreach (var leg in legs)
                {
                    if (leg.flight != null)
                    {
                        OperateLeg(leg);
                    }
                }
                if (legs.Count(leg => leg.flight != null) == 0)
                {
                    IsOperating = false; break;
                }
            }
        }

        public void OperateLeg(Leg leg)
        {
            if (leg.flight == null) return;

            var nextLeg = legs[leg.nextLegId - 1];

            switch (leg.Id)
            {
                case 4 when leg.flight.status == Status.Departure:
                    AddLog(leg.flight, leg);
                    leg.flight = null;
                    break;
                case 8 when leg.flight.status == Status.Arrival && nextLeg.flight == null:
                    leg.flight.status = Status.Departure;
                    nextLeg.flight = leg.flight;
                    AddLog(nextLeg.flight, nextLeg);
                    leg.flight = null;
                    break;
                case 5 when nextLeg.flight != null && legs[6].flight == null:
                    legs[6].flight = leg.flight;
                    AddLog(legs[6].flight, legs[6]);
                    leg.flight = null;
                    break;
                default:
                    if (nextLeg.flight == null)
                    {
                        nextLeg.flight = leg.flight;
                        AddLog(nextLeg.flight, nextLeg);
                        leg.flight = null;
                    }
                    break;
            }
        }

        public void AddLog(Flight flight, Leg leg)
        {
            var entryTime = DateTime.Now;
            Thread.Sleep(leg.CrossingTime);
            var exitTime = DateTime.Now;

            var newLog = new Log
            {
                FlightId = flight.Id,
                LegId = leg.Id,
                Status = flight.status,
                In = entryTime,
                Out = exitTime
            };

            _dataContext.Logs.Add(newLog);
            _dataContext.SaveChanges();
        }


        //public void AddFlight(Flight flight)
        //{
        //    int planesCount = legs.Count(leg => leg.flight != null);


        //    _dataContext.Flights.Add(flight);
        //    _dataContext.SaveChanges();

        //    if (!IsOperating)
        //    {
        //        Start();
        //    }
        //    Timer timer = new Timer(OperateLeg())


        //    //if (legs[0].flight == null && planesCount <= 4)
        //    //{
        //    //    legs[0].flight = flight;
        //    //    await AddLog(flight, legs[0]);
        //    //    _dataContext.Flights.Add(flight);
        //    //    await _dataContext.SaveChangesAsync();
        //    //}


        //}


        //public async Task Start()
        //{
        //    while (true)
        //    {
        //        foreach (var leg in legs)
        //        {
        //            await OperateLeg(leg);
        //        }
        //    }
        //}

        //public async Task OperateLeg(Leg leg)
        //{
        //    var nextLeg = legs[leg.nextLegId];
        //    if (leg.flight != null)
        //    {
        //        await Task.Delay(leg.CrossingTime);
        //        if (leg.flight.status.ToString() == "Arrival")
        //        {
        //            if (leg.Id == 8) { leg.flight.status = Status.Departure; }
        //            nextLeg.flight = leg.flight;
        //            leg.flight = null;
        //            await AddLog(nextLeg.flight, nextLeg);
        //        }
        //        else
        //        {
        //            if (leg.Id == 4) { AddLog(leg.flight, leg); leg.flight = null; }
        //            else
        //            {
        //                nextLeg.flight = leg.flight;
        //                leg.flight = null;
        //                await AddLog(nextLeg.flight, nextLeg);
        //            }

        //        }
        //    }


        //}






        //public void MoveForward()
        //{
        //    for (int i = legs.Count - 1; i >= 0; i--)
        //    {
        //        var currentLeg = legs[i];
        //        if (currentLeg.flight != null)
        //        {
        //            if (currentLeg.Id == 4 && currentLeg.flight.status.ToString() == "Departure")
        //            {
        //                AddLog(currentLeg.flight, currentLeg);
        //                currentLeg.flight = null;
        //            }
        //            else if (currentLeg.Id == 5)
        //            {
        //                if (legs[5].flight == null) { legs[5].flight = currentLeg.flight; AddLog(legs[5].flight, legs[6]); currentLeg.flight = null; }
        //                else if (legs[6].flight == null) { legs[6].flight = currentLeg.flight; AddLog(legs[6].flight, legs[6]); currentLeg.flight = null; }
        //                else { continue; }
        //            }
        //            else if (currentLeg.Id == 8)
        //            {
        //                currentLeg.flight.status = Status.Departure;
        //                if (legs[3].flight != null) { continue; }
        //                else { legs[3].flight = currentLeg.flight; AddLog(legs[3].flight, legs[3]); currentLeg.flight = null; }
        //            }
        //            else
        //            {
        //                if (legs[i + 1].flight == null) { legs[i + 1].flight = currentLeg.flight; AddLog(legs[i + 1].flight, legs[i + 1]); currentLeg.flight = null; }
        //                else { continue; }
        //            }

        //        }
        //    }

        //    _dataContext.SaveChanges();
        //}
    }
}
