using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PMS.Views.Patient
{
    class Form : TemplateForm<object>
    {
        public override string Caption => "Patient";
    }
    class Edit : Form
    {
    }
    class Add : Form
    {

    }
}
