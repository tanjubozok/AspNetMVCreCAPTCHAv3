using AspNetMVCreCAPTCHAv3.Models;
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
        public ActionResult Create(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }
            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}