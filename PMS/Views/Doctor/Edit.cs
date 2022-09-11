using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PMS.Views.Doctor
{
    class Form : TemplateForm<object>
    {
        public override string Caption => "Doctor";
    }
    class Edit : Form
    {
    }
    class Add : Form
    {

    }
}
