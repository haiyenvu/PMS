using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Controllers
{
    class HomeController : BaseController
    {
        public object Default()
        {
            if (Client.IsConnected) { }
            return Redirect("Login");
        }
       
        public object TestMqtt(Models.Patient p)
        {
            return View(new Models.LoginInfo { UserName = p.Name });
        }
        
    }
}
