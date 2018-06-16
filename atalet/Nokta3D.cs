using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace atalet
{
    public class Nokta3D
    {
        public Nokta3D() { }

        public Nokta3D(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
