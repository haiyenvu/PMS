using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Controllers
{
    class MedicalRecordController:BaseController
    {
        public object Default()
        {
            return View(Models.DB.MedicalRecord.Values);
        }

       

        static Models.MedicalRecord selected;
        public object Edit(Models.MedicalRecord e)
        {
            selected = e;
            e = Models.DB.MedicalRecord.Data.FindById<Models.MedicalRecord>(e.PatientID);
            return FormView(e);
        }
        public object Delete()
        {
            Models.DB.MedicalRecord.Delete(selected.PatientID);
            return GoFirst();
        }
        public object Update(DataContext context)
        {
            var e = Json.Convert<Models.MedicalRecord>(context);
            Models.DB.MedicalRecord.UpdateOrInsert(e.PatientID, e);
            return GoFirst();
        }
        public object Import(Models.ImportResult result)
        {
            result.Update(Models.DB.MedicalRecord, e => e.PatientID);
            return GoFirst();
        }

        public object Clear(Models.ImportResult result)
        {
            Models.DB.MedicalRecord.DeleteAll();
            return GoFirst();
        }
    }
}
