using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace PMS.Views.Import
{
    internal class Run : BaseView<ImportResult, Models.ImportResult>
    {
        public override string MainCaption => "Result Import";
        public override object[] GetActions()
        {
            var btn = new ActionButton
            {
                Foreground = Brushes.LimeGreen,
                Text = "Import",
            };
            btn.Click += (s, e) => {
                btn.Request(Model.Name + "/Import", Model);
            };
            return new object[] {
                btn,
                new PasteAction(Model.Name),
            };
        }
        protected override void RenderCore()
        {
            var header = new Grid();
            var gui = (Models.ImportModel)ViewBag["columns"];

            foreach (var s in Model.Headers)
            {
                if (string.IsNullOrEmpty(s)) { continue; }
                MainContent.AddColumn(gui.Columns.First(x => x.Name == s).Caption);
            }

            foreach (var line in Model.Data)
            {
                var lst = new List<string>();
                int i = 0;
                foreach (var s in Model.Headers)
                {
                    if (i >= line.Length) break;

                    var v = line[i++];
                    if (!string.IsNullOrEmpty(s))
                    {
                        lst.Add(v);
                    }
                }
                MainContent.AddRow(lst);
            }
        }
    }
}
