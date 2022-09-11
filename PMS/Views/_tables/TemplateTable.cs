using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace System.Windows.Controls
{
    public class TemplateTable : UserControl
    {
        public event EventHandler ItemSelected;
        protected void RaiseItemSelected(object item)
        {
            if (item != null)
            {
                ItemSelected?.Invoke(item, null);
            }
        }
        DataGrid _grid;
        public TemplateTable()
        {
            _grid = new DataGrid
            {
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
            };

            _grid.MouseDoubleClick += (s, e) => {
                var context = ((FrameworkElement)e.OriginalSource).DataContext;
                RaiseItemSelected(context);
            };

            _grid.KeyUp += (s, e) => {
                if (e.Key == Input.Key.Enter)
                {
                    RaiseItemSelected(_grid.SelectedItem);
                }
            };

            Content = _grid;
        }

        public void LoadTemplate(Models.FieldCollection fields)
        {
            foreach (var p in fields)
            {
                var col = new DataGridTextColumn
                {
                    Binding = new Data.Binding(p.Key),
                    IsReadOnly = true,
                    Header = p.Value.Caption,
                    Width = p.Value.Width ?? 100,
                };
                _grid.Columns.Add(col);
            }
        }

        public System.Collections.IEnumerable ItemsSource
        {
            get => _grid.ItemsSource;
            set => _grid.ItemsSource = value;
        }
    }
}
