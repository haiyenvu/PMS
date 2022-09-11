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
using System.Windows.Shapes;

namespace System.Windows
{
    /// <summary>
    /// Interaction logic for MyMessageBox.xaml
    /// </summary>
    public partial class MyMessageBox : Window
    {
        public MyMessageBox()
        {
            InitializeComponent();

            OK.Click += (s, e) => {
                this.DialogResult = true;
            };
            Cancel.Click += (s, e) => {
                this.DialogResult = false;
            };
        }
        static public bool? Show(string text, string cancel, string ok)
        {
            var frm = new MyMessageBox();
            frm.Message.Text = text;

            if (!string.IsNullOrEmpty(cancel))
            {
                frm.Cancel.Content = cancel;
            }
            if (!string.IsNullOrEmpty(ok))
            {
                frm.Cancel.Margin = frm.OK.Margin;
                frm.OK.Width = frm.Cancel.Width;
                frm.OK.Content = ok;
            }
            return frm.ShowDialog();

        }
        static public bool? Alert(string text)
        {
            return Show(text, null, null);
        }
        static public bool? Confirm(string text)
        {
            return Show(text, "No", "Yes");
        }

    }
}
