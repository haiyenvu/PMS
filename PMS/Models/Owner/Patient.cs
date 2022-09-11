using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Patient : User
    {
       
        public string PatientID => ObjectId;
       
        public string DoctorID
        {
            get => GetString("DoctorID");
            set => SetString("DoctorID", value);
        }
        string _doctorName;
        public string DoctorName
        {
            get
            {
                if (_doctorName == null && !string.IsNullOrEmpty(DoctorID))
                {
                    _doctorName = DB.Doctor.Find(DoctorID)?.GetString("Name");
                }
                return _doctorName;
            }
        }
    }

    public class PatientCollection : DataMap<Patient>
    {

    }
    partial class DB
    {
        static PatientCollection _patient;
        public static PatientCollection Patient
        {
            get
            {
                if (_patient == null)
                {
                    _patient = new PatientCollection();
                    _patient.Load();
                }
                return _patient;
            }
        }
    }


}


