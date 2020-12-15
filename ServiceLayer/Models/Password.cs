using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class Password
    {
        public int? BusinessEntityId;
        public string PasswordHash;
        public string PasswordSalt;
        public Guid Rowguid;
        public DateTime ModifiedDate;

    }
}
