using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class PointLoad
    {
        private double _magnitude;
        public double Location { get; set; }
        public LoadDirection Direction { get; set; }
        public double Magnitude { get; set; }                  
        public PointLoad(double magnitude, double location, LoadDirection direction)
        {
            Location = location;
            Magnitude = magnitude;
            Direction = direction;
        }

    }
}
