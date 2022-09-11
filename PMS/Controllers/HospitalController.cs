using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMS.Controllers
{
    class HospitalController:BaseController
    {
        public object Default()
        {
            //return RedirectToAction("")
            return View(Models.DB.Hospital.Values.OrderBy(e => e.Name));
        }

        public object Add()
        {
            return FormView(selected = new Models.Hospital());
        }

        static Models.Hospital selected;
        public object Edit(Models.Hospital e)
        {
            selected = e;
            e = Models.DB.Hospital.Data.FindById<Models.Hospital>(e.ObjectId);
            return FormView(e);
        }
        public object Delete()
        {
            Models.DB.Hospital.Delete(selected.ObjectId);
            return GoFirst();
        }
        public object Update(DataContext context)
        {
            selected.Copy(context);
            Models.DB.Hospital.UpdateOrInsert(selected);
            return GoFirst();
        }

        public object Import(Models.ImportResult result)
        {
            result.Update(Models.DB.Hospital, e => e.HospitalID);
            return GoFirst();
        }

        public object Clear(Models.ImportResult result)
        {
            Models.DB.Hospital.DeleteAll();
            return GoFirst();
        }

    }
}
