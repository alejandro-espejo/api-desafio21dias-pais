using System;
using api_desafio21dias.ModelViews;
using Microsoft.AspNetCore.Mvc;

namespace api_desafio21dias.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public HomeView Index()
        {
            return new HomeView();
        }

        /*
        [HttpGet]
        public ActionResult Index()
        {
            return Redirect("/swagger");
        }
        */
    }
}