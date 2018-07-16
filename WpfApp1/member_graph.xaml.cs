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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for member_graph.xaml
    /// </summary>
    
    
    public partial class member_graph : UserControl
    {
        public double MyProperty { get => mygrid.ColumnDefinitions[1].ActualWidth + mygrid.ColumnDefinitions[2].ActualWidth; }
        public member_graph()
        {
            InitializeComponent();
        }
    }
}
