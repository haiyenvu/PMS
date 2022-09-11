using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Field
    {
        public string Caption { get; set; }
        public bool? Required { get; set; }
        public double? Width { get; set; }
        public string Input { get; set; }
        public double? InputSize { get; set; }
        public string Options { get; set; }

        public void Copy(Field src)
        {
            foreach (var p in this.GetType().GetProperties())
            {
                if (p.GetValue(this) == null)
                {
                    p.SetValue(this, p.GetValue(src));
                }
            }
        }
    }
    public class GuiMap<T> : Dictionary<string, T>
    {
        new public T this[string key]
        {
            get
            {
                T value;
                base.TryGetValue(key, out value);

                return value;
            }
            set
            {
                if (base.ContainsKey(key))
                {
                    base[key] = value;
                }
                else
                {
                    base.Add(key, value);
                }
            }
        }
    }
    public class FieldCollection : GuiMap<Field>
    {

    }
    public class GuiTemplate : GuiMap<FieldCollection>
    {
        public void CreateFields(FieldCollection fields)
        {
            foreach (var gui in Values)
            {
                foreach (var p in gui)
                {
                    var field = fields[p.Key];
                    if (field != null)
                    {
                        p.Value.Copy(field);
                    }
                }
            }
        }
    }

    public class GUI
    {
        static FieldCollection _fields;
        static GuiTemplate _forms;
        static GuiTemplate _tables;

        public static FieldCollection Fields
        {
            get
            {
                Load();
                return _fields;
            }
        }
        public static GuiTemplate Forms
        {
            get
            {
                Load();
                return _forms;
            }
        }
        public static GuiTemplate Tables
        {
            get
            {
                Load();
                return _tables;
            }
        }

        static void Load()
        {
            if (_fields == null)
            {
                var path = DB.Main.ConnectionString + "/template/";
                _fields = Json.Read<FieldCollection>(path + "fields.json");
                _forms = Json.Read<GuiTemplate>(path + "forms.json");
                _tables = Json.Read<GuiTemplate>(path + "tables.json");

                _forms.CreateFields(_fields);
                _tables.CreateFields(_fields);
            }
        }
    }
}
