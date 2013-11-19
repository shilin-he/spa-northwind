using System;
using System.Web.Mvc;

namespace Northwind.UI.Controllers
{
    public class TemplateController : Controller
    {
        public PartialViewResult Load(string id)
        {
            return PartialView(String.Format("~/Views/Template/{0}.cshtml", id.Replace("-", "/")));
        }
    }
}