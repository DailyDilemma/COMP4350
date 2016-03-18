using System;
using System.Web.Mvc;

namespace ListAssist.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}
