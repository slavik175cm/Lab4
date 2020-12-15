using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class FullPersonInfo
    {
        public Password Password = new Password();
        public Email Email = new Email();
        public Phone Phone = new Phone();
        public Person Person = new Person();
        public Address Address = new Address();
    }
}
