using lib.fizibil.moi;
using lib.fizibil.moi.nokta;
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
        fizibil f = new fizibil();

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

           

            AtaletMomenti moi = f.AtaletMomentHesapla(nokta.ToArray());
            textBox1.Text += "\r\nIy: " + moi.Iy.ToString() + "\r\nIz: " + moi.Iz.ToString() + "\r\nIx: " + moi.Ix.ToString() + "\r\nIxy: " + moi.Ixy.ToString();
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


            AtaletMomenti m = f.AtaletMomentHesapla(nokta.ToArray());
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

            Nokta3D merkez = f.AgirlikMerkezi(nokta.ToArray());

            textBox1.Text = "Cy: " + merkez.Y.ToString() + "\r\nCz: " + merkez.Z.ToString();
            textBox1.Text += "\r\nAlan: " + f.PoligonAlan(nokta.ToArray()).ToString();
            AtaletMomenti m = f.AtaletMomentHesapla(nokta.ToArray());
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


            Nokta3D merkez = f.AgirlikMerkezi(nokta.ToArray());
            textBox1.Text = "Cy: " + merkez.Y.ToString() + "\r\nCz: " + merkez.Z.ToString();
        }
    }
}
