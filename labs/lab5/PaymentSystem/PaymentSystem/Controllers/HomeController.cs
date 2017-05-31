using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace PaySystem.Controllers
{
    public class HomeController : Controller
    {
        private static string _data = "";
        private static string _signature = "";
        public ActionResult Index()
        {
            return View();
        }

        //public string sendPayForm()
        //{
        //    using (var client = new HttpClient())
        //    {

        //        var response = client.PostAsync();
        //    }
        //}

        [HttpPost]
        public ActionResult About(string data, string signature)
        {
            ViewBag.Message = "Your application description page.";

            byte[] base64 = Convert.FromBase64String(data);
            string decodedString = Encoding.UTF8.GetString(base64);

            return View((object)decodedString);
        }

        public void Contact(string data, string signature)
        {
            byte[] base64 = Convert.FromBase64String(data);
            string decodedString = Encoding.UTF8.GetString(base64);
            _data = decodedString;
            _signature = signature;
        }

        [HttpPost]
        public ActionResult SendData(int val)
        {
            var date = new
            {
                version = 3,
                action = "pay",
                public_key = "i27801822639",
                amount = val.ToString(),
                currency = "UAH",
                description = "Мой товар",
                type = "buy",
                sandbox = "1",
                result_url = "http://lab4paysystem.azurewebsites.net/Home/About",
                language = "ru"
            };
            var json = JsonConvert.SerializeObject(date);
            var data = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            var signatureText = "giK9NvA9nrBp7XSUUNfW5A6MCXcUdBLwY2ggIdRI" +
                                data +
                                "giK9NvA9nrBp7XSUUNfW5A6MCXcUdBLwY2ggIdRI";
            var shaCsp = new SHA1CryptoServiceProvider();
            byte[] hash = shaCsp.ComputeHash(Encoding.ASCII.GetBytes(signatureText));
            
            var signature = Convert.ToBase64String(hash);

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("data", data);
            param.Add("signature", signature);

            var bytes = Convert.FromBase64String(
                "eyJ2ZXJzaW9uIjozLCJhY3Rpb24iOiJwYXkiLCJwdWJsaWNfa2V5IjoiaTI3ODAxODIyNjM5IiwiYW1vdW50IjoiMSIsImN1cnJlbmN5IjoiVUFIIiwiZGVzY3JpcHRpb24iOiLQnNC+0Lkg0YLQvtCy0LDRgCIsInR5cGUiOiJidXkiLCJzYW5kYm94IjoiMSIsInNlcnZlcl91cmwiOiJodHRwOi8vbGFiNHBheXN5c3RlbS5henVyZXdlYnNpdGVzLm5ldC9Ib21lL0Fib3V0IiwibGFuZ3VhZ2UiOiJydSJ9");
            var str = Encoding.UTF8.GetString(bytes);

            return View(param);
        }
    }
}