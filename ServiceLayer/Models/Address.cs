using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class Address
    {
        public int AddressId;
        public string AddressLine1;
        public string AddressLine2;
        public string City;
        public int StateProvinceId;
        public string PostalCode;
        public Guid rowguid;
        public DateTime ModifiedDate;
    }
}
