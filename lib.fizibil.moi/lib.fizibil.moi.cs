using lib.fizibil.moi.nokta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lib.fizibil.moi
{
    public class fizibil
    {
        public AtaletMomenti AtaletMomentHesapla(Nokta3D[] Nokta)
        {
            AtaletMomenti atalet = new AtaletMomenti();
            Nokta3D g = AgirlikMerkezi(Nokta);

            int nokta_sayi = Nokta.Length;

            double pay = 0;
            double payda = 0;
            try
            {
                for (int i = 0; i < nokta_sayi - 1; i++)
                {
                    double z0 = Nokta[i].Y - g.Y;
                    double z1 = Nokta[i + 1].Y - g.Y;
                    double y0 = Nokta[i].Z - g.Z;
                    double y1 = Nokta[i + 1].Z - g.Z;

                    atalet.Iy += (y1 - y0) * (z1 + z0) * (Math.Pow(z1, 2) + Math.Pow(z0, 2));
                    atalet.Iz += (z1 - z0) * (y1 + y0) * (Math.Pow(y1, 2) + Math.Pow(y0, 2));
                    //atalet.Ix += (Math.Pow(z0, 2) + Math.Pow(y0, 2) + z0 * z1 + y0 * y1 + Math.Pow(z1, 2) + Math.Pow(y1, 2)) * (z0 * y1 - z1 * y0);

                    pay = ((Math.Pow(z0, 2) + Math.Pow(y0, 2) + z0 * z1 + y0 * y1 + Math.Pow(z1, 2) + Math.Pow(y1, 2)) * (z0 * y1 - z1 * y0));
                    payda = (z0 * y1 - z1 * y0);
                    atalet.Ix += pay / payda;
                    //atalet.Ixy += pay / payda;

                    //atalet.Ixy += (z1 - z0) * (2 * z0 * Math.Pow(y0, 2) + Math.Pow(z1 + y0, 2) * (z1 + z0) + 2 * z1 * Math.Pow(y1, 2));
                    atalet.Ixy += (z1 - z0) * ((2 * z0 * Math.Pow(y0, 2)) + (Math.Pow((z1 + y0), 2) * (z1 + z0)) + (2 * z1 * Math.Pow(y1, 2)));
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
            }

            double alan = PoligonAlan(Nokta);

            atalet.Iy = Math.Round(Math.Abs(atalet.Iy / 12), 4);
            atalet.Iz = Math.Round(Math.Abs(atalet.Iz / 12), 4);
            atalet.Ix = Math.Round(Math.Abs(atalet.Ix / 12), 4);
            //atalet.Ixy = Math.Round(Math.Abs(atalet.Ixy / 24), 4);
            //

            atalet.Ixy = Math.Round((Math.Pow(alan, 4) / (40 * atalet.Ix)), 4);

            return atalet;
        }

        public double PoligonAlan(Nokta3D[] Nokta)
        {
            int nokta_sayi = Nokta.Length;

            double alan = 0;
            for (int i = 0; i < nokta_sayi - 1; i++)
            {
                alan += (Nokta[i + 1].Y - Nokta[i].Y) *
                        (Nokta[i + 1].Z + Nokta[i].Z) / 2;
            }

            return Math.Abs(alan);
        }

        public Nokta3D AgirlikMerkezi(Nokta3D[] Nokta)
        {
            int nokta_sayi = Nokta.Length;

            double Y = 0;
            double Z = 0;
            double ortak_kisim;
            for (int i = 0; i < nokta_sayi - 1; i++)
            {
                ortak_kisim = Nokta[i].Y * Nokta[i + 1].Z - Nokta[i + 1].Y * Nokta[i].Z;
                Y += (Nokta[i].Y + Nokta[i + 1].Y) * ortak_kisim;
                Z += (Nokta[i].Z + Nokta[i + 1].Z) * ortak_kisim;
            }

            double alan = PoligonAlan(Nokta);
            Z /= (6 * alan);
            Y /= (6 * alan);

            if (Y < 0)
            {
                Z = -Z;
                Y = -Y;
            }

            return new Nokta3D(0, Y, Z);
        }
    }
}
