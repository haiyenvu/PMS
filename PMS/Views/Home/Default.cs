using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PMS.Views.Home
{
    internal class Default : TemplateForm<object>
    {
        public override string MainCaption => "Patient Management System";
        public override string Caption => "Patient";
        public override string TemplateName => "Patient";
        protected override void RenderCore()
        {
            MainContent.DataContext = Model;
        }
    }
}
