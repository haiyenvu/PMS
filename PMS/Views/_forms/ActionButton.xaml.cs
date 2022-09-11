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
    /// Interaction logic for ActionButton.xaml
    /// </summary>
    public partial class ActionButton : UserControl
    {
        public string Url { get; set; }
        public string Text
        {
            get => caption.Text;
            set => caption.Text = value;
        }
        new public Border Content => (Border)base.Content;
        new public Brush Background
        {
            get => Content.Background;
            set => Content.Background = value;
        }
        new public Brush Foreground
        {
            get => base.Foreground;
            set
            {
                base.Foreground = value;

                Content.BorderBrush = value;
                caption.Foreground = value;
            }
        }
        public event EventHandler Click;
        public void Request(string url, params object[] args)
        {
            System.Mvc.Engine.Execute(url, args);
        }
        public ActionButton()
        {
            InitializeComponent();

            this.MouseLeftButtonUp += (s, e) => {
                Click?.Invoke(this, null);
                if (Url != null) { Request(Url); }
            };
        }
    }

    public class PasteAction : ActionButton
    {
        public PasteAction(string actionName)
        {
            Text = "Clipboard";
            Foreground = Brushes.Orange;

            Click += (s, e) => Request("Import/" + actionName);
        }
    }
    public class DeleteAction : ActionButton
    {
        public DeleteAction(string controllerName)
        {
            Text = "Delete";
            Foreground = Brushes.Red;

            Click += (s, e) => {
                if (MyMessageBox.Confirm("Are you sure delete data?") == true)
                {
                    Request(controllerName + "/Delete");
                }
            };
        }
    }
    public class DeleteAllAction : ActionButton
    {
        public DeleteAllAction(string controllerName)
        {
            Text = "Delete All";
            Foreground = Brushes.Red;

            Click += (s, e) => {
                if (MyMessageBox.Confirm("Are you sure delete all data?") == true)
                {
                    Request(controllerName + "/clear");
                }
            };
        }
    }
}
