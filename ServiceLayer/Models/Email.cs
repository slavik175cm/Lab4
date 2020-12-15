using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class Email
    {
        public int? BusinessEntityId;
        public int? EmailAddressId;
        public string EmailAddress;
        public Guid rowguid;
        public DateTime ModifiedDate;
    }
}
