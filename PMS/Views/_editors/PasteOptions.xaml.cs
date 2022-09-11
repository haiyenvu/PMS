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
    /// Interaction logic for PasteOptions.xaml
    /// </summary>
    public partial class PasteOptions : UserControl, IFormView
    {
        public PasteOptions()
        {
            InitializeComponent();
        }

        Models.ImportModel _model;
        object IFormView.DataContext
        {
            get => _model;
            set
            {
                _model = (Models.ImportModel)value;
                main_content.Children.Clear();
                foreach (var field in _model.Columns)
                {
                    var input = new ImportOptionBox
                    {
                        Caption = field.Caption,
                        ItemsSource = field.Other,
                    };

                    main_content.Children.Add(input);
                }
            }
        }

        DataContext IFormView.EditedValue
        {
            get
            {
                var context = new DataContext();
                foreach (ImportOptionBox input in main_content.Children)
                {
                    var key = input.Caption;
                    var value = input.Text.Trim();

                    if (string.IsNullOrEmpty(value))
                    {
                        value = null;
                    }
                    context.Add(key, value);
                }
                return context;
            }
        }
    }

    internal class PasteView : MyFormView<PasteOptions, Models.ImportModel>
    {
        public override string Caption => "Cấu hình";
        protected override void RaiseUpdate(string url, DataContext context)
        {
            var text = Clipboard.GetText() ?? string.Empty;
            System.Mvc.Engine.Execute("Import/Run", (string)ViewBag["action"], context, text);
        }
    }


}

