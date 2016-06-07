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

namespace TetrisProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Custom_Message_Box : Window
    {
        public Custom_Message_Box(String label_display, String new_title)
        {
            InitializeComponent();
            label.Content = label_display;
            Title = new_title;
        }
    }
}
