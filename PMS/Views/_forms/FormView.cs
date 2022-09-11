
using PMS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace System.Windows.Controls
{ 
    public interface IFormView
    {
        object DataContext { get; set; }
        DataContext EditedValue { get; }
    }
    public abstract class MyFormView<TContent, TModel> : BaseView<FormViewLayout, TModel>
        where TContent : IFormView, new()
    {
        public abstract string Caption { get; }
        protected virtual void RaiseUpdate(string url, DataContext context)
        {
            System.Mvc.Engine.Execute(url, context);
        }
        protected override void RenderCore()
        {
            var content = new TContent
            {
                DataContext = Model,
            };

            MainContent.OK.Click += (s, e) => {
                var data = content.EditedValue;
                if (data != null)
                {
                    RaiseUpdate(ControllerName + "/update", data);
                }
                else
                {
                    MessageBox.Show("Please complete all information.", Caption);
                }
            };

            if (this.GetType().Name == "Edit")
            {

            }

            MainContent.Cancel.Click += (s, e) => {
                System.Mvc.Engine.Execute(ControllerName + "/back");
            };
            MainContent.Text = Caption;
            MainContent.Content = content;
        }
    }

    public abstract class TemplateForm<TModel> : MyFormView<FormContent, TModel>
    {
        public virtual string TemplateName => ControllerName;

        public override object[] GetActions()
        {
            if (this.GetType().Name == "Edit")
            {
                return new object[] { new DeleteAction(ControllerName) };
            }
            return base.GetActions();
        }

        FormContent _form;
        public FormContent Form => _form;
        protected override void RenderCore()
        {
            base.RenderCore();

            _form = (FormContent)MainContent.Content;
            _form.LoadTemplate(Models.GUI.Forms[this.TemplateName]);

            if (Model != null)
            {
                var data = Json.Convert<DataContext>(Model);
                foreach (var p in data)
                {
                    var e = _form.Editors[p.Key];
                    if (e != null)
                    {
                        e.Value = p.Value;
                    }
                }
            }
        }
    }

}
