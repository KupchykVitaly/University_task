using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using WindowsFormsApp2;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Graphics graph;
        public Pen pen1, pen2, pen3;
        private Star lastStar; // Змінна для зберігання останньої створеної зірки
        public int scale = 5;

        public Form1()
        {
            InitializeComponent();
            graph = CreateGraphics();
            pen1 = new Pen(Color.Black);
            pen2 = new Pen(Color.White);
            pen3 = new Pen(Color.Red);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point p1 = new Point(scale * (Star.StarCount * 10 + 10), scale * 30);
            Star star = new Star(p1, 10, 4, this);
            if (lastStar != null)
            {
                star.IncreaseSize(lastStar); // Збільшення розміру з урахуванням останньої зірки
            }
            star.Show();
            lastStar = star; // Зберігаємо останню створену зірку
        }
    }
}
