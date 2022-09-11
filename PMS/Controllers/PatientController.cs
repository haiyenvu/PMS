 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Controllers
{
    internal class PatientController : BaseController
    {
        public object Default()
        {
            return View(Models.DB.Patient.Values.OrderBy(e => e.Name));
        }

        public object Add()
        {
            return FormView(selected = new Models.Patient());
        }

        static Models.Patient selected;
        public object Edit(Models.Patient e)
        {
            selected = e;
            e = Models.DB.Patient.Data.FindById<Models.Patient>(e.ObjectId);
            return FormView(e);
        }
        public object Delete()
        {
            Models.DB.Patient.Delete(selected.ObjectId);
            return GoFirst();
        }
        public object Update(DataContext context)
        {
            selected.Copy(context);
            Models.DB.Patient.UpdateOrInsert(selected);
            return GoFirst();
        }

        public object Import(Models.ImportResult result)
        {
            result.Update(Models.DB.Patient, e => e.ObjectId);
            return GoFirst();
        }

        public object Clear(Models.ImportResult result)
        {
            Models.DB.Patient.DeleteAll();
            return GoFirst();
        }
    }
}
