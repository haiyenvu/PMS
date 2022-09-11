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

namespace System.Windows.Controls
{
    /// <summary>
    /// Interaction logic for ImportResult.xaml
    /// </summary>
    public partial class ImportResult : UserControl
    {


        public ImportResult()
        {
            InitializeComponent();
        }

        public void AddColumn(string text)
        {
            var col = new TextBlock
            {
                Text = text,
            };
           
            col.SetValue(Grid.ColumnProperty, table.ColumnDefinitions.Count);
            table.ColumnDefinitions.Add(new ColumnDefinition());
            table.Children.Add(col);
        }

        public void AddRow(IEnumerable<string> cells)
        {
            int gridRow = table.RowDefinitions.Count;
            table.RowDefinitions.Add(new RowDefinition
            {
                Height = table.RowDefinitions[0].Height
            });

            int i = 0;
            foreach (var v in cells)
            {
                var cell = new TextBlock { Text = v };
                cell.SetValue(Grid.ColumnProperty, i++);
                cell.SetValue(Grid.RowProperty, gridRow);

                table.Children.Add(cell);
            }
        }
    }
}
