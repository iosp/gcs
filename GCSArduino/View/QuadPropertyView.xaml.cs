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
using GCSArduino.Interface;
using GCSArduino.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace GCSArduino.View
{
    /// <summary>
    /// Interaction logic for QuadPropertyView.xaml
    /// </summary>
    public partial class QuadPropertyView : UserControl
    {
        public QuadPropertyView()
        {
            InitializeComponent();
        }
    }
}
