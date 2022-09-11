using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Controllers
{
    class LoginController : BaseController
    {
        public object Default()
        {
            return View();
        }

        public object Update(DataContext context)
        {
            return null;
        }
        public object Login()
        {
            //Publish("account/login", new Models.LoginInfo { UserName = "Admin", Password = "Admin" });
            return View(new Models.LoginInfo { UserName = "vuhaiyen", Password = "123" });
        }
        public void OK(Models.LoginInfo i)
        {
            Publish("account/login", i);
        }
        public void Logout()
        {
            Publish("account/logout", new object { });
        }
    }
}
