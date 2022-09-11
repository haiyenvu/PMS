using System;
using System.Collections.Generic;
using System.Mvc;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PMS.Views.Login
{
    class Default : TemplateForm<Models.LoginInfo>
    {
        public override string Caption => "Login";
        public override string TemplateName => "LoginInfo";
        //protected override void RenderCore()
        //{
        //    MainContent.DataContext = Model;
        //}

        protected override void RaiseUpdate(string url, DataContext context)
        {
            base.RaiseUpdate(url, context);
        }
    }
}
