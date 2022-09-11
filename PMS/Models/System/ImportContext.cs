using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ImportColumns
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public List<string> Other { get; set; } = new List<string>();
        public bool TryPush(string text)
        {
            if (text == Caption || Other.Contains(text))
            {
                return false;
            }

            Other.Insert(0, text);
            return true;
        }
    }
    public class ImportModel : DataContext
    {
        public List<ImportColumns> Columns { get; set; } = new List<ImportColumns>();
    }
    public class ImportCollection : DataMap<ImportModel>
    {

    }

    public class ImportResult
    {
        public string Name { get; set; }
        public string[] Headers { get; set; }
        public IEnumerable<string[]> Data { get; set; }

        public void Update<T>(Action<T> updateItem)
        {
            DataContext record;
            foreach (var cells in Data)
            {
                record = new DataContext();

                for (int k = 0; k < cells.Length && k < Headers.Length; k++)
                {
                    if (Headers[k] == null) { continue; }
                    var v = cells[k].Trim();
                    if (v == string.Empty) { continue; }

                    record.Add(Headers[k], v);
                }
                var e = record.ToObject<T>();
                updateItem(e);
            }
        }
        public void Update<T>(DataMap<T> data, Func<T, string> getId) where T: DataContext
        {
            Update<T>(e => {
                string id = getId(e);
                if (!string.IsNullOrEmpty(id))
                {
                    data.UpdateOrInsert(id, e);
                }
            });
        }
    }

    partial class DB
    {
        static ImportCollection _imports;
        public static ImportCollection Imports
        {
            get
            {
                if (_imports == null)
                {
                    _imports = new ImportCollection();
                    _imports.Load();

                    if (_imports.Count == 0)
                    {
                        foreach (var gui in GUI.Forms)
                        {
                            var model = new ImportModel();
                            foreach (var field in gui.Value)
                            {
                                var column = new ImportColumns
                                {
                                    Name = field.Key,
                                    Caption = field.Value.Caption,
                                };

                                model.Columns.Add(column);
                            }
                            _imports.Insert(gui.Key, model);
                        }
                    }
                }
                return _imports;
            }
        }
    }
}
