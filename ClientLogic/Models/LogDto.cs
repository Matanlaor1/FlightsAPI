using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Models
{
     public class LogDto
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int LegId { get; set; }
        public DateTime In { get; set; }
        public DateTime? Out { get; set; } = null;
    }
}
