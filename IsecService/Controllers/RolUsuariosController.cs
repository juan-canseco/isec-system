using IsecService.Server.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using DataAccess.Server;

namespace IsecService.Controllers
{
    public class RolUsuariosController : Controller
    {
        RolUsuarioRepository rolUserRepo = new RolUsuarioRepository(); 
        public ActionResult Index()
        {
            return View();
        }

        public string Add (string roluser)
        {
            string message = string.Empty;
            var obj = JsonConvert.DeserializeObject<RolUsuarioServer>(roluser);
            obj.Id = rolUserRepo.Last() + 1;
            int result = rolUserRepo.Add(obj);
            switch (result)
            {
                case 0:
                    message = $"No se logro asignar rol";
                    break;
                case 1:
                    message = $"Se asigno rol correctamente";
                    break;
                case 2:
                    message = $"El rol ya esta asignado";
                    break;
                default:
                    break;
            }
            return message;
        }
    }
}