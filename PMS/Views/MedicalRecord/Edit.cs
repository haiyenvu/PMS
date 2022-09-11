using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace PMS.Views.MedicalRecord
{
    class Form : TemplateForm<object>
    {
        public override string Caption => "MedicalRecord";
    }
    class Edit : Form
    {
    }
    class Add : Form
    {

    }
}
