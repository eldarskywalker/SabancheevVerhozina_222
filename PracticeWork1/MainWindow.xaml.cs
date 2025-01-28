using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace PracticeWork1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum FuncVar
        {
            None,
            shx,
            x2,
            ex
        }

        public MainWindow()
        {
            InitializeComponent();
            string imagePath = Environment.CurrentDirectory + @"\Image1.png";
            ImageFormula.Source = new BitmapImage(new Uri(imagePath));
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            valueY.Text = "0";
            valueX.Text = "0";
            Answer.Text = "0";
            RadioShx.IsChecked = false;
            RadioEx.IsChecked = false;
            Radiox2.IsChecked = false;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(valueX.Text, out double x))
            {
                MessageBox.Show("Error parse X");
                return;
            }
            if (!double.TryParse(valueY.Text, out double y))
            {
                MessageBox.Show("Error parse Y");
                return;
            }

            var funcName = DefineFunction();
            if (funcName == FuncVar.None)
            {
                MessageBox.Show("Choose func!");
                return;
            }

            double result = 0;
            if (x > y)
            {
                result = Math.Pow((Fx(funcName, x) - y), 3) + Math.Atan(Fx(funcName, x));
            }
            else if (x < y)
            {
                result = Math.Pow(y - (Fx(funcName, x)), 3) + Math.Atan(Fx(funcName, x));
            }
            else // =
            {
                result = Math.Pow(y + Fx(funcName, x), 3) + 0.5;
            }

            Answer.Text = result.ToString();
        }

        private FuncVar DefineFunction()
        {
            if (RadioShx.IsChecked ?? false)
            {
                return FuncVar.shx;
            }
            else if (Radiox2.IsChecked ?? false)
            {
                return FuncVar.x2;
            }
            else if (RadioEx.IsChecked ?? false)
            {
                return FuncVar.ex;
            }
            else
            {
                return FuncVar.None;
            }
        }

        private double Fx(FuncVar funcVar, double x)
        {
            double result = 0;
            switch (funcVar)
            {
                case FuncVar.shx:
                    result = Math.Sinh(x);
                    break;
                case FuncVar.x2:
                    result = Math.Pow(x, 2);
                    break;
                case FuncVar.ex:
                    result = Math.Exp(x);
                    break;
            }
            return result;
        }
    }
}