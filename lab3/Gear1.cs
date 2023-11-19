using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public class Star
    {
        public static int StarCount = 0;
        private Point center;
        private int radius;
        private Form1 form;
        private double angle = 0;
        private double spikeLength;
        private int spikeCount;
        private int lastRadius = 10; // Зберігаємо розмір останньої створеної зірки
        public double Angle { get { return angle; } }

        public Star(Point c, double r, int spikes, Form1 form1)
        {
            ++StarCount;
            center = c;
            radius = form1.scale * (int)r;
            form = form1;
            spikeLength = radius * 0.2;
            spikeCount = spikes;
        }

        public class FourPointedStar
        {
            private int a; // Параметр a
            private int b; // Параметр b
            private Form1 form;

            public FourPointedStar(int a, int b, Form1 form1)
            {
                this.a = a;
                this.b = b;
                form = form1;
            }

            public void Draw(Pen pen, Graphics graph)
            {
                if (graph != null)
                {
                    Point[] starPoints = new Point[8];
                    double angle = Math.PI / 4; // Кут для розташування точок
                    double step = 2 * Math.PI / 8; // Крок між точками

                    for (int i = 0; i < 8; i++)
                    {
                        double x = form.ClientSize.Width / 2 + (a * Math.Cos(angle));
                        double y = form.ClientSize.Height / 2 + (b * Math.Sin(angle));
                        starPoints[i] = new Point((int)x, (int)y);
                        angle += step;
                    }

                    graph.DrawPolygon(pen, starPoints);
                }
            }

        }
        public void Show() { Draw(form.pen1, form.graph); }
        public void Hide() { Draw(form.pen2, form.graph); }
    
    public void IncreaseSize(Star lastStar)
        {
            radius = (int)(lastStar.radius * 1.1); // Збільшуємо розмір зірки на 10%
            spikeLength = radius * 0.2; // Оновлюємо розмір кінців
        }
    }
}
