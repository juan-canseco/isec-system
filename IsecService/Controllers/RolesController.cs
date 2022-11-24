using DataAccess.Server;
using IsecService.Server.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsecService.Controllers
{
    public class RolesController : Controller
    {

        RolRepository rolRepo = new RolRepository(); 
        // GET: Roles
        public ActionResult Index()
        {
            return View();
        }

        public string Roles()
        {
            return JsonConvert.SerializeObject(rolRepo.GetAll());
        }
        public int Add(string rol)
        {
            var obj = JsonConvert.DeserializeObject<RolServer>(rol);
            return rolRepo.Add(obj); 
        }
       
    }
}