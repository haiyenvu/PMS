using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Admin : User
    {
        public object CreateUser(DataContext context)
        {
            return CreateMQTTResponse(Account.CreateAccount(context), null, null);
        }
    }
}
