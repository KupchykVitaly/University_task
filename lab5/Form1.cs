using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {
        XmlSerializer xmlSerial;
        FileStream fs;
        SandClock sandclock;
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            Type type = typeof(SandClock);
            MethodInfo[] mi = type.GetMethods();
            foreach (MethodInfo m in mi)
            {
                listBox1.Items.Add(m.ToString());
            }
        }

        [Serializable]
        public class SandClock
        {
            private double a;
            private double b;

            public double A { get { return a; } set { a = value; } }
            public double B { get { return b; } set { b = value; } }

            public SandClock()
            {
                a = 0;
                b = 0;
            }

            public SandClock(double _a, double _b)
            {
                a = _a;
                b = _b;
            }

            public void Draw(Graphics g, double x, double y)
            {
                g.Clear(Color.Empty);
                Pen pn = new Pen(Color.Black, 5); // Змінено товщину контуру на 2

                PointF[] topTriangle = setTopTrianglePoints(x, y);
                PointF[] bottomTriangle = setBottomTrianglePoints(x, y);

                g.DrawPolygon(pn, topTriangle);
                g.DrawPolygon(pn, bottomTriangle);
                g.FillPolygon(new SolidBrush(Color.White), topTriangle);
                g.FillPolygon(new SolidBrush(Color.White), bottomTriangle);
            }


            public PointF[] setTopTrianglePoints(double x, double y)
            {
                PointF[] points = new PointF[3];
                points[0] = new PointF((float)(x - a / 2), (float)(y - b / 2));
                points[1] = new PointF((float)(x + a / 2), (float)(y - b / 2));
                points[2] = new PointF((float)x, (float)(y));
                return points;
            }

            public PointF[] setBottomTrianglePoints(double x, double y)
            {
                PointF[] points = new PointF[3];
                points[0] = new PointF((float)(x - a / 2), (float)(y + b / 2));
                points[1] = new PointF((float)(x + a / 2), (float)(y + b / 2));
                points[2] = new PointF((float)x, (float)(y));
                return points;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sandclock = new SandClock(double.Parse(textBoxA.Text), double.Parse(textBoxB.Text));
            double x = pictureBox1.ClientSize.Width / 2.0;
            double y = pictureBox1.ClientSize.Height / 2.0;
            if (sandclock != null)
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    sandclock.Draw(g, x, y);
                }
                pictureBox1.Image = bmp;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            sandclock = new SandClock(double.Parse(textBoxA.Text), double.Parse(textBoxB.Text));
            xmlSerial = new XmlSerializer(typeof(SandClock));
            fs = new FileStream("sandclock.xml", FileMode.OpenOrCreate);
            xmlSerial.Serialize(fs, sandclock);
            fs.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sandclock = new SandClock();
            if (File.Exists("sandclock.xml"))
            {
                fs = new FileStream("sandclock.xml", FileMode.Open);
                xmlSerial = new XmlSerializer(typeof(SandClock));
                sandclock = (SandClock)xmlSerial.Deserialize(fs);
                fs.Close();
            }
            textBoxA.Text = sandclock.A.ToString();
            textBoxB.Text = sandclock.B.ToString();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (File.Exists("sandclock.dat"))
            {
                fs = new FileStream("sandclock.dat", FileMode.Open);
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    double a = reader.ReadDouble();
                    double b = reader.ReadDouble();
                    sandclock = new SandClock(a, b);
                }
                fs.Close();
                textBoxA.Text = sandclock.A.ToString();
                textBoxB.Text = sandclock.B.ToString();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            sandclock = new SandClock(double.Parse(textBoxA.Text), double.Parse(textBoxB.Text));
            fs = new FileStream("sandclock.dat", FileMode.OpenOrCreate);
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                writer.Write(sandclock.A);
                writer.Write(sandclock.B);
            }
            fs.Close();
        }
    }
}
