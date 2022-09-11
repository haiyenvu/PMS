using System;
using System.Collections.Generic;
using System.Text;
using System.Mvc;
using Models;

namespace PMS.Controllers
{
    class ResponseController : BaseController
    {
        public void Default()
        {
            var url = Response.Pop<string>("#url");
            var action = GetMethod(url);
            action?.Invoke(this, new object[] { });
        }

        public void account_login()
        {
            Token = Json.Convert<DataContext>(Response["Value"]).Pop<string>("#token");
            var code = Response.GetString("Code");
            DataContext v = Json.Convert<DataContext>(Response["Value"]);
            Current_User = Json.Convert<Models.User>(v);

            if (Current_User.AuthorName == "Patient")
                App.Execute("patient");

            else if (Current_User.AuthorName == "Doctor")
                App.Execute("doctor");
        }
        public void account_changepassword()
        {
            Publish("account/logout", new object { });
           // App.Current.MainPage.DisplayAlert("Success", "Your password has been changed.", "OK");
        }

        public void account_logout()
        {
            Token = null;
            App.Execute("home");
        }

        public void doctor_patientlist()
        {
            var code = Response.GetString("Code");
            var v = Response["Value"];
            v = ConvertArray<List<Patient>>(v);

            App.Execute("patient", v);
        }



    }

}