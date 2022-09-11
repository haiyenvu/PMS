
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
    /// Interaction logic for PageViewLayout.xaml
    /// </summary>
    public partial class PageViewLayout : UserControl
    {
        public PageViewLayout()
        {
            InitializeComponent();
            foreach (ActionButton btn in PageSelector.Children)
            {
                btn.Margin = new Thickness(0, 0, 1, 0);
            }

            PageSizeOption.SelectionChanged += (s, e) => { CreatePages(_items); };
            FirstPage.Click += (s, e) => CurrentIndex = 0;
            LastPage.Click += (s, e) => CurrentIndex = _pages.Count - 1;
            NextPage.Click += (s, e) => { if (_currentIndex < _pages.Count - 1) CurrentIndex++; };
            PrevPage.Click += (s, e) => { if (_currentIndex > 0) CurrentIndex--; };
        }

        int _currentIndex = -1;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;

                    CurrentPageButton.Text = string.Format($"{_currentIndex + 1} / {_pages.Count}");
                    CurrentPageChanged?.Invoke(this, null);
                }
            }
        }
        public PageContext CurrentPage => _currentIndex >= _pages.Count ? null : _pages[_currentIndex];

        public event EventHandler CurrentPageChanged;

        List<PageContext> _pages;

        System.Collections.IEnumerable _items;
        public void CreatePages(System.Collections.IEnumerable items)
        {
            _currentIndex = -1;
            _items = items;
            _pages = new List<PageContext>();

            int pageSize = GetPageSize();

            var lst = new PageContext();
            foreach (var item in items)
            {
                lst.Add(item);
                if (lst.Count == pageSize)
                {
                    _pages.Add(lst);
                    lst = new PageContext();
                }
            }
            if (lst.Count > 0)
            {
                _pages.Add(lst);
            }
            CurrentIndex = 0;
        }
        int GetPageSize()
        {
            var text = (string)((ComboBoxItem)PageSizeOption.SelectedValue).Content;
            int a = 0;
            foreach (var c in text)
            {
                if (c < '0' || c > '9') { break; }
                a = (a << 1) + (a << 3) + (c & 15);
            }
            return a;
        }
    }
    public class PageContext : List<object>
    {
    }

    public class PageView<TContent, TModel> : BaseView<PageViewLayout, IEnumerable<TModel>>
        where TContent : UserControl, new()
    {

        protected void OnListViewCreated()
        {
            var control = (TContent)MainContent.ListContent.Child;
            var lstView = (DataGrid)control.Content;
            lstView.CanUserDeleteRows = false;
            lstView.CanUserAddRows = false;
            lstView.MouseDoubleClick += (s, e) =>
            {
                var context = ((FrameworkElement)e.OriginalSource).DataContext;
                if (context != null) { RaiseItemSelected(context); }
            };
        }

        protected virtual void RaiseItemSelected(object item)
        {
            System.Mvc.Engine.Execute(ControllerName + "/edit", item);
        }
        protected virtual void RaisePageSelected(TContent control, PageContext page)
        {
            var lstView = (DataGrid)control.Content;
            lstView.Dispatcher.InvokeAsync(() =>
            {
                lstView.ItemsSource = null;
                lstView.ItemsSource = page;
            });
        }
        protected override void RenderCore()
        {
            var control = new TContent();
            MainContent.ListContent.Child = control;
            MainContent.CurrentPageChanged += (s, e) => {
                RaisePageSelected(control, MainContent.CurrentPage);
            };

            OnListViewCreated();
            MainContent.CreatePages(Model);
        }
    }

    public class PageDataView : BaseView<PageViewLayout, object>
    {
        public override object[] GetActions()
        {
            return new object[] {
                new ActionButton { Text = "Add", Url = ControllerName + "/Add" },
                new PasteAction(ControllerName),
                new DeleteAllAction(ControllerName),
            };
        }
        protected override void RenderCore()
        {
            var table = new TemplateTable();
            table.LoadTemplate(Models.GUI.Tables[ControllerName]);

            MainContent.ListContent.Child = table;
            MainContent.CurrentPageChanged += (s, e) => {
                table.ItemsSource = MainContent.CurrentPage;
            };

            MainContent.CreatePages((System.Collections.IEnumerable)Model);

            table.ItemSelected += (s, e) => {
                RaiseItemSelected(s);
            };
        }
        protected virtual void RaiseItemSelected(object item)
        {
            System.Mvc.Engine.Execute(ControllerName + "/edit", item);
        }

    }
}
