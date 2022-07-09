using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEWC.API.Doamin.Module
{
    public class User
    {
        public int user_ID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public String DisplayName { get; set; }
        public String PasswordSalt { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public String CreatedBy { get; set; }

    }
}
