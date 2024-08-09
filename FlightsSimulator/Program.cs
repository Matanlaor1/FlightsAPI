using System;
using System.Timers;
using ClientLogic;
using ClientSide.Models;

namespace FlightsSimulator
{
    public class Program
    {
        public static FlightService service = new FlightService();
        public static int cap = 0;
        private static System.Timers.Timer timer;
        private static Random random = new Random();

        static void Main(string[] args)
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += OnTimedEvent;
            SetRandomInterval();
            timer.Start();
            Console.ReadLine();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            SendFlight();
            SetRandomInterval();
        }

        private static void SendFlight()
        {
            //if (cap == 20)
            //{
            //    timer.Stop();
            //    return;
            //}

            var flight = new FlightDto { number = Guid.NewGuid() };
            service.AddFlight(flight);
            Console.WriteLine($"Flight added at {DateTime.Now:HH:mm:ss.fff}");
            cap++;
        }

        private static void SetRandomInterval()
        {
            timer.Interval = random.Next(5000, 10001);
        }
    }
}
