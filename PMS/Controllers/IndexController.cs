using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Controllers
{
    class IndexController:BaseController
    {
        public object Default()
        {
            return View(Models.DB.Index.Values);
        }
    }
}
