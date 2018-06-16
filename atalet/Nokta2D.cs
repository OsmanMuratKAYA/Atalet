using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace atalet
{
    public class Nokta2D
    {
        public Nokta2D() { }

        public Nokta2D(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
