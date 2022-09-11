using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PMS.Views.Home
{
    class TestMqtt : BaseView<LoginForm, Models.LoginInfo>
    {
        protected override void RenderCore()
        {
            MainContent.DataContext = Model;
        }
    }
}
