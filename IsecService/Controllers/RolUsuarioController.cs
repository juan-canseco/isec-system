using DataAccess.Server;
using IsecService.Server.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace IsecService.Controllers
{
    [RoutePrefix("api/userrole")]
    public class RolUsuarioController : ApiController
    {
        RolUsuarioRepository rolUserRepo = new RolUsuarioRepository();
        [HttpGet]
        [Route("all")]
        public JsonResult<List<RolUsuarioServer>> all()
        {
            return Json(rolUserRepo.GetAllByRol(0));
        }
    }
}
