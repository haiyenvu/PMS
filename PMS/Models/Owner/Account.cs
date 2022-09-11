using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Account : BaseModel
    {
        public string UserName
        {
            get => GetString("UserName");
            set => SetString("UserName", value);
        }
        public string Password
        {
            get => GetString("Password");
            set => SetString("Password", value);
        }
        //public string Token { get; set; }
        public string AuthorName
        {
            get => GetString("AuthorName");
            set => SetString("AuthorName", value);
        }
        public DateTime LastLogin { get; set; }

       bool CheckAuthor(string author)
       {
            return (string.IsNullOrEmpty(AuthorName));
        }
        protected DataContext CreateMQTTResponse(int code, string message, object value)
        {
            var context = new DataContext();
            context.Push("Code", code);
            context.Push("Message", message);
            context.Push("Value", value);

            return context;
        }
        protected virtual object Ok(object value)
        {
            return CreateMQTTResponse(0, null, value);
        }
        protected virtual object Error(int code, string message)
        {
            return CreateMQTTResponse(code, message, null);
        }
        public object Login()
        {
            //var acc = context.ChangeType<Account>();

            int code = TryLogin();

            if (code != 0)
            {
                return Error(code, null);
            }
            var user = DB.UserCollection.CreateUser(this);
            return Ok(user);
        }
        int TryLogin()
        {
            var un = UserName.ToLower();
            var acc = DB.Accounts.FindById<Account>(un);
            if (acc == null)
            {
                return -1;
            }

            if (acc.Password != un.JoinMD5(Password).ToMD5())
            {
                return -2;
            }

            this.Copy(acc);
            this.Remove("Password");
            return 0;
        }
        public static int CreateAccount(string userName, string password, string authorname)
        {
            var db = DB.Accounts;
            if (db.Contains(userName))
            {
                return -1;
            }

            if (DB.UserCollection.GetActor(authorname) == null)
            {
                return -2;
            }

            userName = userName.ToLower();
            if (password == null)
            {
                password = userName;
            }

            var acc = new Account();

            acc.Push("UserName", userName);
            acc.Push("Password", userName.JoinMD5(password).ToMD5());
            acc.Push("AuthorName", authorname);
            DB.Accounts.Insert(userName, acc);
           

            return 0;


        }
        public static int CreateAccount(DataContext context)
        {
            var acc = context.ChangeType<Account>();
            return CreateAccount(acc.UserName, acc.Password, acc.AuthorName);
        }
    }
    partial class DB
    {
        static Collection _accounts;
        public static Collection Accounts
        {
            get
            {
                const string ad = "Doctor";
                if (_accounts == null)
                {
                    _accounts = Main.GetCollection<Account>();
                    Account.CreateAccount(ad, ad, ad);
                }
                return _accounts;
            }
        }
    }

}
