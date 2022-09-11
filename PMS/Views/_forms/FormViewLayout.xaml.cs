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
    /// Interaction logic for FormViewLayout.xaml
    /// </summary>
    public partial class FormViewLayout : UserControl
    {
        public FormViewLayout()
        {
            InitializeComponent();
        }
        public string Text
        {
            get => caption.Text;
            set => caption.Text = value;
        }

        new public object Content
        {
            get => main_content.Child;
            set => main_content.Child = (UIElement)value;
        }
    }
}
