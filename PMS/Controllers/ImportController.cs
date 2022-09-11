using PMS.Controllers;
using System;
using System.Collections.Generic;
using System.Mvc;
using System.Text;

namespace PMS.Controllers
{
    internal class ImportController : BaseController
    {
        static string _backController;
        public override object Back()
        {
            return Redirect(_backController);
        }
        protected object BeginOption(string name)
        {
            ViewData["action"] = _backController = name;
            return View(Models.DB.Imports.Find(name));
        }

        protected object BeginOption()
        {
            return BeginOption(RequestContext.ActionName);
        }
        public object Run(string name, DataContext context, string source)
        {
            #region Create Header Map
            bool changed = false;
            var model = Models.DB.Imports.Find(name);

            var map = new Models.GuiMap<string>();
            foreach (var col in model.Columns)
            {
                var s = (string)context[col.Caption];
                if (s != null)
                {
                    if (col.TryPush(s))
                    {
                        changed = true;
                    }
                }
                else
                {
                    s = col.Caption;
                }

                map.Add(s, col.Name);
            }
            if (changed)
            {
                Models.DB.Imports.Update(name, model);
            }
            #endregion

            #region Split Import Lines
            var lines = source.Split('\n');
            if (lines.Length < 2) { return null; }

            var headers = lines[0].Split('\t');
            for (int i = 0; i < headers.Length; i++)
            {
                var s = headers[i].Trim();
                if (s == string.Empty)
                {
                    headers[i] = null;
                    continue;
                }
                headers[i] = map[s];
            }
            #endregion

            #region Create Import Data
            var data = new List<string[]>();
            for (int i = 1; i < lines.Length; i++)
            {
                var cells = lines[i].Trim().Split('\t');
                data.Add(cells);
            }
            var lst = new Models.ImportResult
            {
                Headers = headers,
                Name = name,
                Data = data,
            };
            #endregion

            ViewData["columns"] = model;

            return View(lst);
        }
        public object Patient()
        {
            return BeginOption();
        }
        public object Doctor()
        {
            return BeginOption();
        }
        public object Hospital()
        {
            return BeginOption();
        }
        public object MedicalRecord()
        {
            return BeginOption();
        }
    }
}
