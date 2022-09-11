using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PMS.Views.Hospital
{
    class Form : TemplateForm<object>
    {
        public override string Caption => "Hospital";
    }
    class Edit : Form
    {
    }
    class Add : Form
    {

    }
}
