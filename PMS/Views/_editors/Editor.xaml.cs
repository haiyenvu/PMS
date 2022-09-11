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
using PMS.Views;

namespace System.Windows.Controls
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public interface IEditor
    { 
        
        object Value { get; set; }
        string Name { get; set; }
        string Text { get; set; }
        double Size { get; set; }
    }

    public partial class TextInput : UserControl, IEditor
    {
        public TextInput()
        {
            InitializeComponent();
        }

        

        public Brush BorderColor
        {
            get => ((Border)base.Content).BorderBrush;
            set => ((Border)base.Content).BorderBrush = value;
        }

        protected virtual object GetValueCore() { return ((TextBox)Control.Child).Text.Trim(); }
        protected virtual void SetValueCore(object value) { ((TextBox)Control.Child).Text = value?.ToString(); }
        public object Value
        {
            get
            {
                object v = GetValueCore();
                if (v != null && v.Equals(string.Empty)) v = null;

                return v;
            }
            set => SetValueCore(value);
        }
        public string Text
        {
            get => Caption.Text;
            set => Caption.Text = value;
        }
        public double Size { get; set; } = 100;
       
    }

    public class PasswordInput : TextInput
    {
        PasswordBox _box;
        public PasswordInput()
        {
            Control.Child = _box = new PasswordBox();
        }
        protected override object GetValueCore()
        {
            return _box.Password;
        }
        protected override void SetValueCore(object value)
        {
            _box.Password = value?.ToString();
        }
    }

    public class ComboInput : TextInput
    {
        ComboBox _combo;
        public ComboInput()
        {
            Control.Child = _combo = new ComboBox();
        }
        protected override object GetValueCore()
        {
            return _combo?.SelectedValue;
        }
        protected override void SetValueCore(object value)
        {
            _combo.Text = value?.ToString();
        }
        public string Options
        {
            set
            {
                foreach (var s in value.Split(';'))
                {
                    _combo.Items.Add(s.Trim());
                }
            }
        }
    }
}
