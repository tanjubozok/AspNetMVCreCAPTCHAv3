using AspNetMVCreCAPTCHAv3.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AspNetMVCreCAPTCHAv3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Create()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {

                var isCaptchaValid = await IsCaptchaValid(model.GoogleCaptchaToken);
                if (isCaptchaValid)
                {

                    return RedirectToAction("Success");
                }
                else
                {
                    ModelState.AddModelError("GoogleCaptcha", "Captcha doğrulanmadı.");
                }

            }
            return View(model);
        }

        private async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                var secret = "secretkey";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"secret",secret},
                        {"response",response},
                        {"remoteip",Request.UserHostAddress},
                    };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseModel>(captchaResponseJson);
                    return captchaResult.Success && captchaResult.Action == "create" && captchaResult.Score > 0.5;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}