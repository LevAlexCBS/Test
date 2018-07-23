using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ZPointLoad
    {
        private double _magnitude;
        public double Magnitude { get; set; }
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
        public ZPointLoad(double magnitude, double location)
        {
            Location = location;
            Magnitude = magnitude;
        }

    }
}
