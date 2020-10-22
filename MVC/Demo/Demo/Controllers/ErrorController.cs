using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{StatusCode}")]
        public ViewResult PageNotFound(int StatusCode)
        {
            ViewBag.ErrorMassage = $"Error {StatusCode}: Sorry the resource you requested could not be found";
            return View();
        }
        [Route("Error")]
        public ViewResult Error()
        {
            return View("Exception");
        }
    }
}
