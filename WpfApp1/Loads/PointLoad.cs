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
        public double Magnitude { get; set; }
        public LoadDirection Direction { get; set; }
        public double Location
        {
            get
            {
                return _magnitude;
            }
            set
            {
                if (value < 0)
                    _magnitude = 0;
                else if (value > 1000)
                    _magnitude = 1000;
                else
                    _magnitude = value;                    
            }
        }
        public PointLoad(double magnitude, double location, LoadDirection direction)
        {
            Location = location;
            Magnitude = magnitude;
            Direction = direction;
        }

    }
}
