using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PMS.Views
{
    public class FormContent : StackPanel, IFormView
    {
        class Row : StackPanel
        {
            public Row() { Orientation = Orientation.Horizontal; }
        }

        Row _lastRow;
        double _available;

        GuiMap<IEditor> _editors = new GuiMap<IEditor>();
        public GuiMap<IEditor> Editors => _editors;

        void CreateRow()
        {
            _available = 100;
            Children.Add(_lastRow = new Row());
        }

        protected override Size MeasureOverride(Size constraint)
        {
            foreach (var editor in _editors.Values)
            {
                var e = (UserControl)editor;
                e.Width = constraint.Width * editor.Size / 100;
            }
            return base.MeasureOverride(constraint);
        }

        public DataContext EditedValue
        {
            get
            {
                var context = new DataContext();

                foreach (var p in Editors)
                {
                    object v = p.Value.Value;
                    if (p.Value.Text.EndsWith("(*)") && v == null)
                    {
                        return null;
                    }
                    context.Add(p.Key, v);
                }
                return context;
            }
        }

        public FormContent()
        {
            CreateRow();
        }

        public UIElement Add(string name, IEditor editor)
        {
            _editors.Add(name, editor);
            _available -= editor.Size;

            if (_available < 0)
            {
                CreateRow();
                _available -= editor.Size;
            }

            var e = (UIElement)editor;
            _lastRow.Children.Add(e);

            return e;
        }
        public IEditor CreateInput(string type)
        {
            if (string.IsNullOrEmpty(type)) type = "Text";

            var e = (IEditor)Activator.CreateInstance(Type.GetType("System.Windows.Controls." + type + "Input"));
            return e;
        }
        public void LoadTemplate(Models.FieldCollection fields)
        {
            foreach (var p in fields)
            {
                var editor = CreateInput(p.Value.Input);
                var caption = p.Value.Caption;

                if (p.Value.Required != false) caption += " (*)";

                editor.Text = caption;
                editor.Size = p.Value.InputSize ?? 100;

                if (p.Value.Input == "Combo")
                {
                    ((ComboInput)editor).Options = p.Value.Options;
                }

                this.Add(p.Key, editor);
            }
        }
    }
}
