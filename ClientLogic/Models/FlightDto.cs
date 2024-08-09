using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Models
{
    public class FlightDto
    {
        public int Id { get; set; }
        public Guid number { get; set; }
        public Status status { get; set; }
    }
}
