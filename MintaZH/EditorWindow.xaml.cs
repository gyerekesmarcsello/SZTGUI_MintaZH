using MintaZH.Models;
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
using System.Windows.Shapes;

namespace MintaZH
{
    /// <summary>
    /// Interaction logic for EditorWindow.xaml
    /// </summary>
    public partial class EditorWindow : Window
    {
        public EditorWindow()
        {
            InitializeComponent();
        }

        public EditorWindow(IList<Fixture> fixtures) : this()
        {
            foreach (var f in fixtures)
            {
                Label l = new Label();
                l.Content = f.Name + "\n" + f.Type + "\n" + f.Cost;
                l.BorderThickness = new Thickness(2);
                l.BorderBrush = Brushes.Black;
                l.Background = Brushes.LightYellow;

                stack.Children.Add(l);
            }

            Label l1 = new Label();
            l1.Content = "All Cost: " + fixtures.Sum(t => t.Cost);
            l1.BorderThickness = new Thickness(2);
            l1.BorderBrush = Brushes.Black;
            l1.Background = Brushes.LightBlue;


            Label l2 = new Label();
            l2.Content = "All Count: " + fixtures.Count();
            l2.BorderThickness = new Thickness(2);
            l2.BorderBrush = Brushes.Black;
            l2.Background = Brushes.LightBlue;

            stack.Children.Add(l1);
            stack.Children.Add(l2);
        }
    }
}
