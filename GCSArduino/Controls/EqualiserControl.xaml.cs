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
using GCSArduino.Converter;

namespace GCSArduino.Controls
{
    /// <summary>
    /// Interaction logic for EqualiserControl.xaml
    /// </summary>
    public partial class EqualiserControl : UserControl
    {
        public EqualiserControl()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            //var size = StackPanel.Height/4;
            for (int i = 0; i < 256 / 3; i++) // 256 step 2
            {
                var rectangle = new Rectangle();
                var binding = new Binding("Value") { ConverterParameter = i * 3, Converter = new EqualiserColorConverter() };

                rectangle.SetBinding(Shape.FillProperty, binding);
                rectangle.Height = 1.5;
                rectangle.Margin = new Thickness(1);
                rectangle.Stretch = Stretch.Fill;
                rectangle.StrokeLineJoin = PenLineJoin.Round;
                rectangle.VerticalAlignment = VerticalAlignment.Bottom;

                StackPanel.Children.Add(rectangle);

            }
            var textBlock = new TextBlock();
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.FontSize = 10;
            var bindingTextBlock = new Binding("Id");
            textBlock.SetBinding(TextBlock.TextProperty, bindingTextBlock);


            Grid.SetRow(textBlock, 2);
            Grid.SetColumn(textBlock, 0);
            Grid.Children.Add(textBlock);

            var textBlock1 = new TextBlock();
            textBlock1.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock1.FontSize = 10;
            var bindingTextBlock1 = new Binding("Value");
            textBlock1.SetBinding(TextBlock.TextProperty, bindingTextBlock1);


            Grid.SetRow(textBlock1, 0);
            Grid.SetColumn(textBlock1, 0);


            Grid.Children.Add(textBlock1);

            var checkBox = new CheckBox
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                IsChecked = false
            };

            var bindingcheckBox = new Binding("IsChecked");
            checkBox.SetBinding(CheckBox.IsCheckedProperty, bindingcheckBox);

            Grid.SetRow(checkBox, 3);
            Grid.SetColumn(checkBox, 0);
            Grid.Children.Add(checkBox);


        }
    }
}
