using IsecService.Server.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsecService.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        UsuarioRepository userRepo = new UsuarioRepository();
        public ActionResult Index()
        {
            return View();
        }
        public string Login(string username,string password)
        { 
            var user = userRepo.Login(username, password); 
            return JsonConvert.SerializeObject(user);
        }
    }
}