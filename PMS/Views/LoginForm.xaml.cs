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

namespace PMS.Views
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : UserControl
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string un = txtUserName.Text.Trim();
            string ps = txtPassword.Password.Trim();
            if (un.Length == 0)
            {
                MessageBox.Show("Enter your username");
                txtUserName.Focus();
                return;

            }
             
            if (ps.Length == 0)
            {
                MessageBox.Show("Enter your password");
                txtPassword.Focus();
                return;
            }
            
            App.Execute("home/LoginClick", new Models.LoginInfo { 
                UserName = un,
                Password = ps,
            });

        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
