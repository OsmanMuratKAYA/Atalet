using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace atalet
{
    public class AtaletMoment
    {
        public AtaletMoment() { }

        public AtaletMoment(double Ixy, double Ix, double Iy, double Iz)
        {
            this.Ixy = Ixy;
            this.Ix = Ix;
            this.Iy = Iy;
            this.Iz = Iz;
        }

        public double Ixy { get; set; }
        public double Ix { get; set; }
        public double Iy { get; set; }
        public double Iz { get; set; }
    }
}
