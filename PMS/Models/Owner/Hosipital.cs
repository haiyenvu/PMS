using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Hospital : BaseModel
    {
        
        public string HospitalID => ObjectId;

        public string Name => GetString("Name");
        public Address Address => GetObject<Address>("Address");
        public string PhoneNumber => GetString("PhoneNumber");
        public string Email => GetString("Email");
    }
    public class Address
    {
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string Ward { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public override string ToString()
        {
            return String.Format("{0}, Phường {1}, Quận {2}, Thành phố {3}", Details, Ward, District, City);
        }
    }

    public class HospitalCollection : DataMap<Hospital>
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
        static HospitalCollection _hospital;
        public static HospitalCollection Hospital
        {
            get
            {
                if (_hospital == null)
                {
                    _hospital = new HospitalCollection();
                    _hospital.Load();
                }
                return _hospital;
            }
        }
    }

}

