using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class Person
    {
        public int BusinessEntityId;
        public string PersonType;
        public bool NameStyle;
        public string Title;
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public string Suffix;
        public int EmailPromotion;
        public string AdditionalContactInfo;
        public string Demographics;
        public Guid rowguid;
        public DateTime ModifiedDate;
    }
}
