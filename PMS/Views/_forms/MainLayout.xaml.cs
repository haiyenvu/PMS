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
    /// Interaction logic for MainLayout.xaml
    /// </summary>
    public partial class MainLayout : UserControl
    {
        public MainLayout()
        {
            InitializeComponent();

            btnMenu.MouseLeftButtonUp += (s, e) => {
                var col = splitPanel.ColumnDefinitions[0];
                col.Width = new GridLength(col.Width.Value == 0 ? col.MaxWidth : 0);
            };
        }
        public bool UpdateView(IAppView view)
        {
            var content = (UIElement)view.Content;
            if (content == null) { return false; }

            main_caption.Dispatcher.InvokeAsync(() => {
                string caption = view.MainCaption?.ToUpper();
                if (!string.IsNullOrEmpty(caption))
                {
                    main_caption.Content = caption;
                }
            });

            actionMenu.Dispatcher.InvokeAsync(() => {
                actionMenu.Children.Clear();
                foreach (UIElement e in view.GetActions())
                {
                    actionMenu.Children.Add(e);
                }
            });

            main_content.Dispatcher.InvokeAsync(() => main_content.Child = content);
            return true;
        }
    }
}
