using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SampleWebsite.ViewModels.MyTest;
using SampleWebsite.Attributes;

namespace SampleWebsite.Controllers {
    public class MyTestController : Controller {
        // GET: /<controller>/
        [UseParsleyValidation]
        public IActionResult Submitted() => View();

        [HttpPost]
        [UseParsleyValidation]
        public IActionResult Submitted(FirstViewModel model) {
            if (ModelState.IsValid) {
                // can do other stuff here
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return View(model);
        }
    }
}
