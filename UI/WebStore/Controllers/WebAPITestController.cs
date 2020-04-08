using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;

namespace WebStore.Controllers
{
    public class WebAPITestController : Controller
    {
        private readonly IValuesService _Values;

        public WebAPITestController(IValuesService Values) => _Values = Values;
        public async Task<IActionResult> Index()
        {
            IEnumerable<string> values = await _Values.GetAsync();
            return View(values);
        }
    }
}