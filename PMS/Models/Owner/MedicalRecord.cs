using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MedicalRecord : BaseModel
    {
        public string PatientID => GetString("PatientID");
        public string DiseaseName { get; set; }
        string _patientName;
        public string PatientName
        {
            get
            {

                if (_patientName == null && !string.IsNullOrEmpty(PatientID))
                {
                    _patientName = DB.Patient.Find(PatientID)?.GetString("Name");
                }
                return _patientName;
            }
        }


        List<Index> _listIndex;

        public List<Index> ListIndex
        {
            get
            {
                if (_listIndex == null)
                {
                    _listIndex = new List<Index>();
                }
                return _listIndex;
            }
        }

        public void AddIndex(Index v)
        {
            _listIndex.Add(v);
        }
        public void RemoveIndex(Index v)
        {
            _listIndex.Remove(v);
        }

        public int CheckWarning()
        {
            int maxWn = 0;
            foreach (var v in _listIndex)
            {
                int wn = v.CheckWarning();
                if (wn > maxWn)
                {
                    maxWn = wn;
                }
            }
            return maxWn;
        }
    }

    public class Index: MedicalRecord
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public DateTime LastMeasure { get; set; }

        
    }

    public class MedicalRecordCollection : DataMap<MedicalRecord>
    {
        DataContextFilterPath _filter;
        public DataContextFilterPath Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filter = new DataContextFilterPath("Doctor", "Patient");
                    _filter.Start(this.Values, "Doctor");

                }
                return _filter;
            }
        }

    }
    partial class DB
    {
        static MedicalRecordCollection _medicalRecord;
        public static MedicalRecordCollection MedicalRecord
        {
            get
            {
                if (_medicalRecord == null)
                {
                    _medicalRecord = new MedicalRecordCollection();
                    _medicalRecord.Load();
                }
                return _medicalRecord;
            }
        }
    }
    public class IndexCollection : DataMap<Index>
    {

    }
    partial class DB
    {
        static IndexCollection _index;
        public static IndexCollection Index
        {
            get
            {
                if (_index == null)
                {
                    _index = new IndexCollection();
                    _index.Load();
                }
                return _index;
            }
        }
    }

}
