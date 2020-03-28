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

namespace Notifications
{
    /// <summary>
    /// Interaction logic for Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
            InitializeComponent();
        }

        public void LoadParameters(Models.Parameters parameters)
        {
            Title = parameters.Title;
            if (parameters.Maximized)
                WindowState = WindowState.Maximized;

            if(parameters.BgColor.Length > 0)
            {
                try
                {
                    var rgb = parameters.BgColor.Split(';').Select(x => byte.Parse(x)).ToArray();
                    Background = new SolidColorBrush(Color.FromRgb(rgb[0], rgb[1], rgb[2]));
                }
                catch { }

            }

            if (parameters.Gradient.Length > 0)
            {
                try
                {
                    var rgba = parameters.Gradient.Split('|')[0].Split(';').Select(x => byte.Parse(x)).ToArray();
                    var rgbb = parameters.Gradient.Split('|')[1].Split(';').Select(x => byte.Parse(x)).ToArray();
                    var angle = double.Parse(parameters.Gradient.Split('|')[2]);
                    Background = new LinearGradientBrush(
                        Color.FromRgb(rgba[0], rgba[1], rgba[2]), 
                        Color.FromRgb(rgbb[0], rgbb[1], rgbb[2]), 
                        angle);
                }
                catch { }

            }

            if (parameters.BgImage.Length > 0)
            {
                try
                {
                    if (System.IO.File.Exists(parameters.BgImage))
                    {
                        var img = new BitmapImage(new Uri(parameters.BgImage));

                        if (parameters.Autosize)
                        {
                            Height = img.Height;
                            Width = img.Width;
                        }

                        var brush = new ImageBrush(img);
                        brush.Stretch = Stretch.Fill;
                        Background = brush;
                    }
   
                }
                catch { }

            }


            if (parameters.Width > 0)
                Width = parameters.Width;

            if (parameters.Height > 0)
                Height = parameters.Width;

            

            Show();
        }
    }
}
