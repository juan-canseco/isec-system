using IsecService.Server.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DataAccess;

namespace IsecService.Controllers
{
    public class UsuariosController : Controller
    {

        UsuarioRepository userRepo = new UsuarioRepository(); 
        // GET: Usuarios
        public ActionResult Index()
        { 
            return View();
        }

        public string All()
        {
            return JsonConvert.SerializeObject(userRepo.GetAll());
        }
        public int Add(string user)
        {
            var userParse = JsonConvert.DeserializeObject<Usuario>(user); 
            userParse.Id =  userRepo.GetLastFolio()+1;
            return userRepo.Add(userParse); 
        }
    }
}