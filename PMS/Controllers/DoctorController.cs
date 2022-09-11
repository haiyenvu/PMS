using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace PMS.Controllers
{
    internal class DoctorController : BaseController
    {
        public object Default()
        {
            Publish("doctor/patientlist", new object());
            return View(Models.DB.Doctor.Values.OrderBy(e => e.Name));
        }

        public object Add()
        {
            return FormView(selected = new Models.Doctor());
        }

        static Models.Doctor selected;
        public object Edit(Models.Doctor e)
        {
            selected = e;
            e = Models.DB.Doctor.Data.FindById<Models.Doctor>(e.ObjectId);
            return FormView(e);
        }
        public object Delete()
        {
            Models.DB.Doctor.Delete(selected.ObjectId);
            return GoFirst();
        }
        public object Update(DataContext context)
        {
            selected.Copy(context);
            Models.DB.Doctor.UpdateOrInsert(selected);
            return GoFirst();
        }

        public object Import(Models.ImportResult result)
        {
            result.Update(Models.DB.Doctor, e => e.ObjectId);
            return GoFirst();
        }

        public object Clear(Models.ImportResult result)
        {
            Models.DB.Doctor.DeleteAll();
            return GoFirst();
        }
    }
}
