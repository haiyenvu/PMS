using System;
using System.Collections.Generic;
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

namespace System.Windows.Controls
{
    /// <summary>
    /// Interaction logic for NavMenuItem.xaml
    /// </summary>
    public partial class NavMenuItem : UserControl
    {
        new UIElement Background => (UIElement)((Grid)this.Content).Children[0];
        public NavMenuItem()
        {
            InitializeComponent();

            this.MouseMove += (s, e) => Background.Visibility = Visibility.Visible;
            this.MouseLeave += (s, e) => Background.Visibility = Visibility.Hidden;

            this.MouseLeftButtonUp += (s, e) => {
                if (string.IsNullOrEmpty(Url) == false)
                {
                    System.Mvc.Engine.Execute(Url);
                }
            };
        }

        public string Text
        {
            get => caption.Text;
            set => caption.Text = value;
        }
        public string Url { get; set; }
    }
}
