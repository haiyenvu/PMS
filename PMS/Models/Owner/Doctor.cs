using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Doctor : User
    {
        public string DoctorID => ObjectId;
        public string Specialization 
        { 
            get => GetString("Specialization");
            set => SetString("Specialization", value);
        
        }
        List<Patient> _patients;

        public List<Patient> Patients
        {
            get
            {
                if (_patients == null)
                {
                    _patients = new List<Patient>();
                }
                return _patients;
            }
        }


        public void AddPatient(Patient p)
        {
            _patients.Add(p);
        }
        public void RemovePatient(Patient p)
        {
            _patients.Remove(p);
        }
    }
    public class DoctorCollection : DataMap<Doctor>
    {

    }
    partial class DB
    {
        static DoctorCollection _doctor;
        public static DoctorCollection Doctor
        {
            get
            {
                if (_doctor == null)
                {
                    _doctor = new DoctorCollection();
                    _doctor.Load();
                }
                return _doctor;
            }
        }
    }

}
