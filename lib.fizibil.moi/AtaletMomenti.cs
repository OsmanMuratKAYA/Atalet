using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.fizibil.moi
{
    public class AtaletMomenti
    {
        public AtaletMomenti() { }

        public AtaletMomenti(double Ixy, double Ix, double Iy, double Iz)
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
