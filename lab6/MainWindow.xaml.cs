using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtCatet1.Text, out double catet1) && double.TryParse(txtCatet2.Text, out double catet2))
            {
                double hypotenuse = Math.Sqrt(catet1 * catet1 + catet2 * catet2);
                double area = 0.5 * catet1 * catet2;

                txtHypotenuse.Text = hypotenuse.ToString("F2");
                txtArea.Text = area.ToString("F2");
            }
            else
            {
                MessageBox.Show("Введіть коректні значення для катетів.");
            }

        }

    }
}
