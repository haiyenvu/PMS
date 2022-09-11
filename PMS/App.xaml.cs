using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace PMS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void Execute(string url, params object[] args)
        {
            if (url != null)
            {
                System.Mvc.Engine.Execute(url, args);
            }
        }
        public App()
        {
            var layout = new System.Windows.Controls.MainLayout();

            object currentView = null;
            System.Mvc.Engine.Register(this, result => {

                var view = result.View;
                if (view.Content is Window)
                {
                    ((Window)view.Content).ShowDialog();
                    return;
                }

                if (layout.UpdateView((IAppView)view))
                {
                    ((IDisposable)currentView)?.Dispose();
                    currentView = view;
                }
            });

            MainWindow = new Window
            {
                Title = " Patient Management System ",
                Visibility = Visibility.Visible,
                Width = 800,
                Height = 500,
                Content = layout
            };

            System.Mvc.Engine.Execute("home");
        }
        protected override void OnExit(ExitEventArgs e)
        {
            System.Mvc.Engine.Exit();
            base.OnExit(e);
        }
    }
}
