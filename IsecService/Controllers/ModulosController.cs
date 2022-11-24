using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsecService.Server.Repositorios;
using Newtonsoft.Json;
using DataAccess;
using DataAccess.Server;
namespace IsecService.Controllers
{
    public class ModulosController : Controller
    {
        ModuloRepository moduloRepo = new ModuloRepository();
        AccesoUsuarioRepository permisoRepo = new AccesoUsuarioRepository();
        public ActionResult Index()
        {
            return View();
        }
        public string All(int rol)
        {
            return JsonConvert.SerializeObject(null);
        }
    }
}