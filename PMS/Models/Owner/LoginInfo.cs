using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class LoginInfo : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
