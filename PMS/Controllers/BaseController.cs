using System;
using System.Collections.Generic;
using System.Text;
using System.Mvc;
using Models;
using uPLibrary.Networking.M2Mqtt.Messages;
using uPLibrary.Networking.M2Mqtt;
using Newtonsoft.Json.Linq;

namespace PMS.Controllers
{
    class BaseController : Controller
    {
        //for ResponseController 
        public DataContext Response { get; set; }
        const string _topic = "do-an-tot-nghiep/20211";
        public string PrivateTopic => _topic + '/' + ClientId;
        public static string Token { get; set; }
        public static User Current_User { get; set; }

        static string _clientId;
        public static string ClientId
        {
            get
            {
                if (_clientId == null)
                {
                    _clientId = Guid.NewGuid().ToString();
                }
                return _clientId;
            }
        }
        static MqttClient _mqttClient;
        public MqttClient Client
        {
            get
            {
                if (_mqttClient == null)
                {
                    _mqttClient = new MqttClient(
                    "broker.emqx.io",
                    1883,
                    false,
                    MqttSslProtocols.None,
                    null,
                    null
                );

                    ConnectMqtt(5);
                }
                return _mqttClient;
            }
        }
        T GetMqttMessage<T>(MqttMsgPublishEventArgs e)
        {
            string content = System.Text.Encoding.UTF8.GetString(e.Message);
            var context = Newtonsoft.Json.Linq.JObject
                .Parse(content)
                .ToObject<T>();
            return context;
        }
        byte[] GetEncodeBytes(object v)
        {
            var content = Newtonsoft.Json.Linq.JObject.FromObject(v).ToString();
            return System.Text.Encoding.UTF8.GetBytes(content);
        }
        void MqttMsgReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var context = GetMqttMessage<DataContext>(e);

            var c = Engine.GetController<BaseController>("Response");
            if (c != null)
            {
                var action = c.GetMethod("Default");
                if (action != null)
                {
                    c.Response = context;
                    AsyncEngine.CreateThread(() => action.Invoke(c, new object[] { }));
                }
            }
        }
        protected void ConnectMqtt(int checkConnectionSeconds = 0)
        {
            if (_mqttClient != null && _mqttClient.IsConnected) return;
            _mqttClient.MqttMsgPublishReceived += MqttMsgReceived;

            string topic = MQTT.ResponseContext.DefaultTopic + _clientId;
            _mqttClient.Connect(ClientId);
            _mqttClient.ConnectionClosed += (s, e) =>
            {
                Screen.Warning("Connection closed");
            };

            if (_mqttClient.IsConnected)
            {
                Screen.Write("done");
                Subscribe(PrivateTopic);
                Subscribe(_topic);
                Publish("account/login", new Models.LoginInfo { UserName = "vuhaiyen", Password = "123" });
            }

            if (checkConnectionSeconds > 0)
            {
                int interval = checkConnectionSeconds * 1000;
                AsyncEngine.CreateThread(() =>
                {
                   while (true)
                   {
                       System.Threading.Thread.Sleep(interval);
                        ConnectMqtt();
                   }
               });
            }
        }
        protected void Subscribe(string topic)
        {
            _mqttClient.Subscribe(new string[] { topic }, new byte[] { 0 });
        }
        protected void Publish(string topic, string url, object value)
        {
            if (_mqttClient == null || _mqttClient.IsConnected == false)
            {
                ConnectMqtt();
            }
            if (_mqttClient.IsConnected)
            {
                var context = value == null ? new DataContext() : DataContext.FromObject(value);
                context.SetString("#url", url);
                context.SetString("#client-id", ClientId);
                if (!string.IsNullOrEmpty(Token))
                {
                    context.SetString("#token", Token);
                }
                _mqttClient.Publish(topic, GetEncodeBytes(context));
            }
        }
        protected void Publish(string url, object value)
        {
            Publish(_topic, url, value);
        }
        protected void Disconnect()
        {
            if (_mqttClient != null && _mqttClient.IsConnected)
            {
                _mqttClient.Disconnect();
            }
        }
        public virtual object Back()
        {
            return GoFirst();
        }
        public object GoFirst()
        {
            return RedirectToAction("Default");
        }
        public object GoHome()
        {
            return Redirect("Home");
        }
        protected object FormView(object model)
        {
            return View(model);
        }
        protected override ActionResult View(IView view, object model)
        {
            ViewData["controller"] = RequestContext.ControllerName;
            return base.View(view, model);
        }
        protected T GetValue<T>(object value) { return Json.Convert<T>(value); }
        protected T ConvertArray<T>(object value)
        {
            var jarray = JArray.FromObject(value);
            return jarray.ToObject<T>();
        }
    }
}
