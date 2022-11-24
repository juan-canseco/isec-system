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
    public class AccesosUsuarioController : Controller
    {

        AccesoUsuarioRepository accesoRepo = new AccesoUsuarioRepository(); 
        public ActionResult Index()
        {
            return View();
        }
        public string AllByRol(int id)
        {
             return JsonConvert.SerializeObject(accesoRepo.GetAllByRol(id)); 
        }
        public void Update(string permiso)
        {
            var obj = JsonConvert.DeserializeObject<AccesoUsuarioServer>(permiso);
            accesoRepo.Updated(obj);
        }
    }
}