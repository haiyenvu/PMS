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
    /// Interaction logic for HorizontalCaptionInput.xaml
    /// </summary>
    public partial class HorizontalCaptionInput : UserControl
    {
        public HorizontalCaptionInput()
        {
            InitializeComponent();
        }

        public string Caption
        {
            get => caption.Text;
            set => caption.Text = value;
        }
        public string Text
        {
            get => textContent.Text;
            set => textContent.Text = value;
        }
    }
}
