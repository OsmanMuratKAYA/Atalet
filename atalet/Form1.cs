using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace atalet
{
    public partial class Form1 : Form
    {

        //metod1: Ahmet Hoca pdf çarpanlar
        //atalet.Iy += Nokta[i + 1].Y * Math.Pow(Nokta[i + 1].Z, 3) +
        //             Nokta[i + 1].Y * Nokta[i].Z * Math.Pow(Nokta[i + 1].Z, 2) -
        //             Nokta[i].Y * Math.Pow(Nokta[i + 1].Z, 3) -
        //             Nokta[i].Y * Nokta[i].Z * Math.Pow(Nokta[i + 1].Z, 2) +
        //             Nokta[i + 1].Y * Nokta[i + 1].Z * Math.Pow(Nokta[i].Z, 2) +
        //             Nokta[i + 1].Y * Math.Pow(Nokta[i].Z, 3) -
        //             Nokta[i].Y * Nokta[i + 1].Z * Math.Pow(Nokta[i].Z, 2) -
        //             Nokta[i].Y * Math.Pow(Nokta[i].Z, 3);

        //metod2: Ahmet Hoca pdf aynı

        //atalet.Iy += ((Nokta[i + 1].Y - g.Y) - (Nokta[i].Y - g.Y)) * ((Nokta[i + 1].Z - g.Z) + (Nokta[i].Z - g.Z)) * (Math.Pow((Nokta[i + 1].Z - g.Z), 2) + Math.Pow((Nokta[i].Z - g.Z), 2));
        //atalet.Iz += ((Nokta[i + 1].Z - g.Z) - (Nokta[i].Z - g.Z)) * ((Nokta[i + 1].Y - g.Y) + (Nokta[i].Y - g.Y)) * (Math.Pow((Nokta[i + 1].Y - g.Y), 2) + Math.Pow((Nokta[i].Y - g.Y), 2));
        //atalet.Ixy += ((Nokta[i + 1].Z - g.Z) - (Nokta[i].Z - g.Z)) + 
        //    (2 * (Nokta[i].Z - g.Z) * Math.Pow((Nokta[i].Y - g.Y), 2) + 

        //    Math.Pow(((Nokta[i + 1].Z - g.Z) + (Nokta[i].Y - g.Y)), 2) * ((Nokta[i + 1].Z - g.Z) - (Nokta[i].Z - g.Z)) + 
        //    2 * (Nokta[i + 1].Z - g.Z) * Math.Pow((Nokta[i + 1].Y - g.Y), 2));

        //metod3 Green's teoremi, mathworks
        //atalet.Iy += (Math.Pow(Nokta[i].Z, 2) + Nokta[i].Z * Nokta[i + 1].Z + Math.Pow(Nokta[i + 1].Z, 2)) * (Nokta[i].Z * Nokta[i + 1].Y - Nokta[i + 1].Z * Nokta[i].Y);
        //atalet.Iz += (Math.Pow(Nokta[i].Y, 2) + Nokta[i].Y * Nokta[i + 1].Y + Math.Pow(Nokta[i + 1].Y, 2)) * (Nokta[i].Z * Nokta[i + 1].Y - Nokta[i + 1].Z * Nokta[i].Y);
        //pay += ((Math.Pow(Nokta[i].Z, 2) + Math.Pow(Nokta[i].Y, 2) + Nokta[i].Z * Nokta[i + 1].Z + Nokta[i].Y * Nokta[i + 1].Y + Math.Pow(Nokta[i + 1].Z, 2) + Math.Pow(Nokta[i + 1].Y, 2)) * (Nokta[i].Z * Nokta[i + 1].Y - Nokta[i + 1].Z * Nokta[i].Y));
        //payda += (Nokta[i].Z * Nokta[i + 1].Y - Nokta[i + 1].Z * Nokta[i].Y);


        public Form1()
        {
            InitializeComponent();
        }

        private double PoligonAlan(Nokta3D[] Nokta)
        {
            int nokta_sayi = Nokta.Length;

            double alan = 0;
            for (int i = 0; i < nokta_sayi-1; i++)
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

            return new Nokta3D(0,Y,Z);
        }

        public AtaletMoment AtaletMomentHesapla(Nokta3D[] Nokta)
        {
            AtaletMoment atalet = new AtaletMoment();
            Nokta3D g = AgirlikMerkezi(Nokta);
            
            int nokta_sayi = Nokta.Length;
  
            double pay = 0;
            double payda = 0;
            try
            {
                for (int i = 0; i < nokta_sayi-1 ; i++)
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
                    textBox2.Text += "\r\nZ0=" + z0.ToString() + ", Z1=" + z1.ToString() + ", Y0=" + y0.ToString() + ", Y1=" + y1.ToString() + ", Ixy=" + atalet.Ixy.ToString();

                }
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.ToString();
            }

            double alan = PoligonAlan(Nokta);

            atalet.Iy = Math.Round(Math.Abs(atalet.Iy / 12),4);
            atalet.Iz = Math.Round(Math.Abs(atalet.Iz / 12),4);
            atalet.Ix = Math.Round(Math.Abs(atalet.Ix / 12), 4);
            //atalet.Ixy = Math.Round(Math.Abs(atalet.Ixy / 24), 4);
            //

            atalet.Ixy = Math.Round((Math.Pow(alan, 4) / (40 * atalet.Ix)), 4);

            return atalet;
        }



        private void button1_Click(object sender, EventArgs e)
        {

            List<Nokta3D> nokta = new List<Nokta3D>();
            //Nokta3D p1 = new Nokta3D(0, 25, -15);
            //Nokta3D p2 = new Nokta3D(0, -25, -15);
            //Nokta3D p3 = new Nokta3D(0, -25, 15);
            //Nokta3D p4 = new Nokta3D(0, 25, 15);
            //Nokta3D p5 = new Nokta3D(0, 25, -15);

            Nokta3D p1 = new Nokta3D(0, 0, 0);
            Nokta3D p2 = new Nokta3D(0, 50, 0);
            Nokta3D p3 = new Nokta3D(0, 50, 30);
            Nokta3D p4 = new Nokta3D(0, 0, 30);
            Nokta3D p5 = new Nokta3D(0, 0, 0);
            nokta.Add(p1);
            nokta.Add(p2);
            nokta.Add(p3);
            nokta.Add(p4);
            nokta.Add(p5);

            Nokta3D merkez = AgirlikMerkezi(nokta.ToArray());
      
            textBox1.Text += "Cy: " + merkez.Y.ToString() + "\r\nCz: " + merkez.Z.ToString();
            textBox1.Text += "\r\nAlan: "+ PoligonAlan(nokta.ToArray()).ToString();
            AtaletMoment m = AtaletMomentHesapla(nokta.ToArray());
            textBox1.Text += "\r\nIy: " + m.Iy.ToString() + "\r\nIz: " + m.Iz.ToString() + "\r\nIx: " + m.Ix.ToString() + "\r\nIxy: " + m.Ixy.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //b=30,h=50,t1=t2=10
            List<Nokta3D> nokta = new List<Nokta3D>();
            Nokta3D p1 = new Nokta3D(0, 10, 0);
            Nokta3D p2 = new Nokta3D(0, 20, 0);
            Nokta3D p3 = new Nokta3D(0, 20, 40);
            Nokta3D p4 = new Nokta3D(0, 30, 40);
            Nokta3D p5 = new Nokta3D(0, 30, 50);
            Nokta3D p6 = new Nokta3D(0, 0, 50);
            Nokta3D p7 = new Nokta3D(0, 0, 40);
            Nokta3D p8 = new Nokta3D(0, 10, 40);
            Nokta3D p9 = new Nokta3D(0, 10, 0);

            nokta.Add(p1);
            nokta.Add(p2);
            nokta.Add(p3);
            nokta.Add(p4);
            nokta.Add(p5);
            nokta.Add(p6);
            nokta.Add(p7);
            nokta.Add(p8);
            nokta.Add(p9);

            Nokta3D merkez = AgirlikMerkezi(nokta.ToArray());

            textBox1.Text = "Cy: " + merkez.Y.ToString() + "\r\nCz: " + merkez.Z.ToString();
            textBox1.Text += "\r\nAlan: " + PoligonAlan(nokta.ToArray()).ToString();
            AtaletMoment m = AtaletMomentHesapla(nokta.ToArray());
            textBox1.Text += "\r\nIy: " + m.Iy.ToString() + "\r\nIz: " + m.Iz.ToString() + "\r\nIx: " + m.Ix.ToString() + "\r\nIxy: " + m.Ixy.ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<Nokta3D> nokta = new List<Nokta3D>();

            Nokta3D p9 = new Nokta3D(0, 0, 5);
            Nokta3D p8 = new Nokta3D(0, 0, 25);
            Nokta3D p7 = new Nokta3D(0, 10, 25);
            Nokta3D p6 = new Nokta3D(0, 10, 20);
            Nokta3D p5 = new Nokta3D(0, 20, 20);
            Nokta3D p4 = new Nokta3D(0, 20, 0);
            Nokta3D p3 = new Nokta3D(0, 10, 0);
            Nokta3D p2 = new Nokta3D(0, 10, 5);
            Nokta3D p1 = new Nokta3D(0, 0, 5);
            nokta.Add(p1);
            nokta.Add(p2);
            nokta.Add(p3);
            nokta.Add(p4);
            nokta.Add(p5);
            nokta.Add(p6);
            nokta.Add(p7);
            nokta.Add(p8);
            nokta.Add(p9);

            Nokta3D merkez = AgirlikMerkezi(nokta.ToArray());

            textBox1.Text = "Cy: " + merkez.Y.ToString() + "\r\nCz: " + merkez.Z.ToString();
            textBox1.Text += "\r\nAlan: " + PoligonAlan(nokta.ToArray()).ToString();
            AtaletMoment m = AtaletMomentHesapla(nokta.ToArray());
            textBox1.Text += "\r\nIy: " + m.Iy.ToString() + "\r\nIz: " + m.Iz.ToString() + "\r\nIx: " + m.Ix.ToString() + "\r\nIxy: " + m.Ixy.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Nokta3D> nokta = new List<Nokta3D>();

            Nokta3D p1 = new Nokta3D(0, 0, 0);
            Nokta3D p2 = new Nokta3D(0, 27, 0);
            Nokta3D p3 = new Nokta3D(0, 0, 18);

            nokta.Add(p1);
            nokta.Add(p2);
            nokta.Add(p3);


            Nokta3D merkez = AgirlikMerkezi(nokta.ToArray());
            textBox1.Text = "Cy: " + merkez.Y.ToString() + "\r\nCz: " + merkez.Z.ToString();
        }
    }
}
