using IsecService.Models;
using IsecService.Server.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using DataAccess;
using DataAccess.Local;
namespace IsecService.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuarioController : ApiController
    {

        UsuarioRepository userRepo = new UsuarioRepository();

        [HttpGet]
        [Route("all")]
        public JsonResult< List<Usuario>> all()
        {
            return Json(userRepo.GetAll());
        } 
        [HttpPost]
        [Route("sync")]
        public JsonResult< List<int>>  sync([FromBody]List<UsuarioLocal> noSync)
        {
            return Json(userRepo.UsersNoSync(noSync));
        }
        [HttpPost]
        [Route("syncInserted")]
        public JsonResult<int> syncInserted([FromBody]UsuarioLocal usuarioLocal)
        {
            userRepo.AddUserInserted(usuarioLocal);
            return Json(1);
        }
        [HttpPost]
        [Route("syncUpdated")]
        public JsonResult<int> syncUpdated([FromBody]UsuarioLocal usuarioLocal)
        {
            userRepo.AddUserUpdated(usuarioLocal);
            return Json(1);
        }
        [HttpGet]
        [Route("Login")]
        public JsonResult<Usuario> Login(string username,string password)
        {
            return Json(userRepo.Login(username,password));
        }
    }
}
